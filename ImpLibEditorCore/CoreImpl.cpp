#include "CoreInterfaces_h.h"

#include <atlbase.h>
#include <atlcom.h>
#include <atlstr.h>
#include <atlconv.h>
#include <algorithm>
#include <cstdio>

// {6FA595B7-0FB8-4FB0-BBB1-4ACD307393E8}
GUID CLSID_CImpLibWalker = 
{ 0x6fa595b7, 0xfb8, 0x4fb0, { 0xbb, 0xb1, 0x4a, 0xcd, 0x30, 0x73, 0x93, 0xe8 } };

// {E04AF0D0-6D00-432E-8B35-65529EC9F9F0}
GUID CLSID_CImpMember = 
{ 0xe04af0d0, 0x6d00, 0x432e, { 0x8b, 0x35, 0x65, 0x52, 0x9e, 0xc9, 0xf9, 0xf0 } };

#define _Module (*_pModule)

struct NameTypeMap {
    int nRaw;
    ImportNameType nCustom;
};

static NameTypeMap s_map[] = {
    { IMPORT_OBJECT_ORDINAL, INT_Ordinal },
    { IMPORT_OBJECT_NAME, INT_KeepUnchanged },
    { IMPORT_OBJECT_NAME_NO_PREFIX, INT_NoPrefix },
    { IMPORT_OBJECT_NAME_UNDECORATE, INT_UnDecorate },
    { 0, INT_Undef }
};

class CImpLibWalker;

struct CharStr {
    typedef char *CHARPTR;
    static CHARPTR AllocStr(int nLen) { return new char[nLen + 1]; }
};

struct WCharStr { 
    typedef wchar_t *CHARPTR;
    static CHARPTR AllocStr(int nLen) { return new wchar_t[nLen + 1]; }
};

struct BSTRStr {
    typedef BSTR CHARPTR;
    static CHARPTR AllocStr(int nLen) { return SysAllocStringByteLen(0, nLen * sizeof(OLECHAR)); }
};

template<class CHART>
void SymbolNameToImportName(const typename CHART::CHARPTR pIn, typename CHART::CHARPTR* pOut, ImportNameType imt)
{
    bool bSkPrefix = false;
    bool bTrunAt = false;
    switch (imt) {
        case INT_KeepUnchanged:
            break;
        case INT_NoPrefix:
            bSkPrefix = true;
            break;
        case INT_UnDecorate:
            bSkPrefix = true;
            bTrunAt = true;
            break;
        default:
            *pOut = 0;
            return;
    }

    typename CHART::CHARPTR pszBegin, pszEnd;
    pszBegin = pIn;

    if (bSkPrefix) {
        switch(*pszBegin) {
            case '?':
            case '@':
            case '_':
                ++pszBegin;
        }
    }

    pszEnd = pszBegin;
    if (bTrunAt) {
        for(; *pszEnd != 0; ++pszEnd)
            if (*pszEnd == '@')
                break;
    } else {
        while (*pszEnd != 0)
            ++pszEnd;
    }

    int nLen = pszEnd - pszBegin;
    typename CHART::CHARPTR pszTmpStr = CHART::AllocStr(nLen);
    pszTmpStr[nLen] = 0;
    std::copy(pszBegin, pszEnd, pszTmpStr);

    *pOut = pszTmpStr;
}

class __declspec(uuid("E04AF0D0-6D00-432E-8B35-65529EC9F9F0"))
CImpMember : public CComObjectRootEx<CComMultiThreadModelNoCS>, public CComCoClass<CImpMember, &CLSID_CImpMember>, public IImpMember
{
    char* m_psrcDllName;
    char* m_psrcSymbolName;
    IMPORT_OBJECT_HEADER* m_psrcHeader;

    CComPtr<IImpLibWalker> m_walker;
public:
    DECLARE_CLASSFACTORY()
    BEGIN_COM_MAP(CImpMember)
        COM_INTERFACE_ENTRY_IID(CLSID_CImpMember, CImpMember)
        COM_INTERFACE_ENTRY(IImpMember)
        COM_INTERFACE_ENTRY(IUnknown)
    END_COM_MAP()

    friend class CImpLibWalker;

    CImpMember()
    {
    }

    ~CImpMember()
    {
    }

    HRESULT STDMETHODCALLTYPE get_DllName(BSTR *pOut)
    {
        CA2WEX<> cvt(m_psrcDllName, CP_ACP);
        *pOut = SysAllocString(cvt);
        return S_OK;
    }
        
    HRESULT STDMETHODCALLTYPE get_IsImportByName(VARIANT_BOOL *pOut)
    {
        if (m_psrcHeader->NameType == IMPORT_OBJECT_ORDINAL)
            *pOut = VARIANT_FALSE;
        else
            *pOut = VARIANT_TRUE;
        return S_OK;
    }
        
    HRESULT STDMETHODCALLTYPE get_OrdinalOrHint(short *pOut)
    {
        *pOut = m_psrcHeader->Ordinal;
        return S_OK;
    }

    HRESULT STDMETHODCALLTYPE get_SymbolName(BSTR *pOut)
    {
        CA2WEX<> cvt(m_psrcSymbolName, CP_UTF8);
        *pOut = SysAllocString(cvt);
        return S_OK;
    }

    HRESULT STDMETHODCALLTYPE get_ImportName(BSTR *pOut)
    {
        char* pszTmpStr;

        NameTypeMap* m;
        for(m = s_map; m->nCustom != INT_Undef; ++m) {
            if (m_psrcHeader->NameType == m->nRaw) {
                break;
            }
        }

        if (m->nCustom != INT_Undef) {
            SymbolNameToImportName<CharStr>(m_psrcSymbolName, &pszTmpStr, m->nCustom);
            CA2WEX<> cvt(pszTmpStr, CP_UTF8);
            *pOut = SysAllocString(cvt);
            delete[] pszTmpStr;
            return S_OK;
        } else {
            return S_FALSE;
        }
    }

    HRESULT STDMETHODCALLTYPE get_ImportNameType(ImportNameType *pOut)
    {
        NameTypeMap* m;
        for(m = s_map; m->nCustom != INT_Undef; ++m ) {
            if (m_psrcHeader->NameType == m->nRaw) {
                *pOut = m->nCustom;
                break;
            }
        }
        if (m->nCustom == INT_Undef)
            return S_FALSE;
        else
            return S_OK;
    }

    HRESULT STDMETHODCALLTYPE put_ImportNameType(ImportNameType pIn)
    {
        NameTypeMap* m;
        for(m = s_map; m->nCustom != INT_Undef; ++m) {
            if (pIn == m->nCustom) {
                m_psrcHeader->NameType = m->nRaw;
                break;
            }
        }
        if (m->nCustom == INT_Undef)
            return S_FALSE;
        else
            return S_OK;
    }
};


class __declspec(uuid("6FA595B7-0FB8-4FB0-BBB1-4ACD307393E8"))
CImpLibWalker : public CComObjectRootEx<CComMultiThreadModelNoCS>, public CComCoClass<CImpLibWalker, &CLSID_CImpLibWalker>, public IImpLibWalker
{
    PBYTE m_pData;
    int m_nDataSize;

    PBYTE m_pDataEnd;

    PIMAGE_ARCHIVE_MEMBER_HEADER m_pCurMemHeader;

    bool MoveNext()
    {
        int nSize = 0;
        std::sscanf((char*) m_pCurMemHeader->Size, "%d", &nSize);
        
        PBYTE ptr = (PBYTE)m_pCurMemHeader;
        ptr += sizeof(IMAGE_ARCHIVE_MEMBER_HEADER);
        ptr += nSize;

        if (ptr >= m_pDataEnd) return false;

        while (ptr < m_pDataEnd && *ptr == *IMAGE_ARCHIVE_PAD) ++ptr;

        if (ptr >= m_pDataEnd) return false;

        m_pCurMemHeader = (PIMAGE_ARCHIVE_MEMBER_HEADER) ptr;
        return true;
    }
public:
    DECLARE_REGISTRY(CImpLibWalker, TEXT("ImpLibEditor.CImpLibWalker"), TEXT("ImpLibEditor.CImpLibWalker"), TEXT("SoraYuki, 2014"), THREADFLAGS_BOTH)
    DECLARE_CLASSFACTORY()
    BEGIN_COM_MAP(CImpLibWalker)
        COM_INTERFACE_ENTRY(IImpLibWalker)
        COM_INTERFACE_ENTRY(IUnknown)
    END_COM_MAP()

    CImpLibWalker()
        : m_pData(0), m_nDataSize(0), m_pDataEnd(0)
    {
    }
    
    ~CImpLibWalker()
    {
    }

    HRESULT STDMETHODCALLTYPE SetSource(byte *pData, int nSize)
    {
        //check sign
        if (std::equal(pData, pData + IMAGE_ARCHIVE_START_SIZE, IMAGE_ARCHIVE_START) == false)
            return S_FALSE;

        m_pData = pData + IMAGE_ARCHIVE_START_SIZE;
        m_pCurMemHeader = (PIMAGE_ARCHIVE_MEMBER_HEADER) m_pData;
        m_nDataSize = nSize;

        m_pDataEnd = pData + nSize;

        return S_OK;
    }

    HRESULT STDMETHODCALLTYPE MoveNext(VARIANT_BOOL *pOut)
    {
        if ((PBYTE)m_pCurMemHeader >= m_pDataEnd) {
            return S_FALSE;
        }

        for(;;) {
            if (MoveNext() == true) {
                if (std::equal(m_pCurMemHeader->Name, m_pCurMemHeader->Name + sizeof(m_pCurMemHeader->Name), IMAGE_ARCHIVE_LINKER_MEMBER ) == true)
                    ; //1st/2nd link member
                else if (std::equal(m_pCurMemHeader->Name, m_pCurMemHeader->Name + sizeof(m_pCurMemHeader->Name), IMAGE_ARCHIVE_LONGNAMES_MEMBER ) == true)
                    ; //longname member
                else
                {
                    //check if it's import object
                    PBYTE pMemData = (PBYTE)(m_pCurMemHeader + 1);
                    IMPORT_OBJECT_HEADER* pImpHeader = (IMPORT_OBJECT_HEADER*) pMemData;
                    if (pImpHeader->Sig1 == IMAGE_FILE_MACHINE_UNKNOWN && pImpHeader->Sig2 == IMPORT_OBJECT_HDR_SIG2)
                        break;
                }
            } else {
                *pOut = VARIANT_FALSE;
                return S_OK;
            }
        }

        *pOut = VARIANT_TRUE;
        return S_OK;
    }

    HRESULT STDMETHODCALLTYPE get_Current(IImpMember **pOut)
    {
        if ((PBYTE)m_pCurMemHeader >= m_pDataEnd) return S_FALSE;

        CImpMember* r;
        CImpMember::CreateInstance(&r);
        r->m_walker.Attach(this);
        r->m_psrcHeader = (IMPORT_OBJECT_HEADER*)(m_pCurMemHeader + 1);
        r->m_psrcSymbolName = (char*)(r->m_psrcHeader + 1);

        char* p = r->m_psrcSymbolName;
        while(*p != 0) ++p;
        r->m_psrcDllName = p + 1;

        *pOut = r;
        return S_OK;
    }

    HRESULT STDMETHODCALLTYPE ReWind(VARIANT_BOOL *pOut)
    {
        m_pCurMemHeader = (PIMAGE_ARCHIVE_MEMBER_HEADER) m_pData;
        return S_OK;
    }
        
    HRESULT STDMETHODCALLTYPE Translate(BSTR pIn, ImportNameType nameType, BSTR *pOut)
    {
        SymbolNameToImportName<BSTRStr>(pIn, pOut, nameType);
        if (*pOut != 0)
            return S_OK;
        else
            return S_FALSE;
    }
};

OBJECT_ENTRY_AUTO(CLSID_CImpLibWalker, CImpLibWalker)

static class ImpLibEditorCoreModule : public CAtlDllModuleT<ImpLibEditorCoreModule>
{
} impedtlib;

extern "C"
{
    HRESULT __stdcall DllCanUnloadNow(void)
    {
        return impedtlib.DllCanUnloadNow();
    }

    HRESULT __stdcall DllRegisterServer(void)
    {
        return impedtlib.DllRegisterServer();
    }

    HRESULT __stdcall DllUnregisterServer(void)
    {
        return impedtlib.DllUnregisterServer();
    }

    BOOL __stdcall DllMain(HMODULE hMod, DWORD nReason, LPVOID lpReserved)
    {
        return impedtlib.DllMain(nReason, lpReserved);
    }

    HRESULT __stdcall DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID *ppv)
    {
        return impedtlib.DllGetClassObject(rclsid, riid, ppv);
    }

    IImpLibWalker* __stdcall CreateImpLibWalker()
    {
        IImpLibWalker* r;
        CImpLibWalker::CreateInstance(&r);
        return r;
    }
};
