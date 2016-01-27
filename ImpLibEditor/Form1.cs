using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using CoreInterfaces;

namespace ImpLibEditor
{
    public partial class Form1 : Form
    {
        IImpLibWalker m_lib;
        byte[] m_fileData;
        GCHandle m_pinHandle;

        [DllImport("ImpLibEditorCore.dll", EntryPoint="CreateImpLibWalker", CallingConvention=CallingConvention.StdCall)]
        static extern IImpLibWalker CreateImpLibWalker();

        class LVIImportItem : ListViewItem
        {
            IImpMember impl;

            public IImpMember Impl 
            {
                get { return impl; }
            }

            public LVIImportItem(IImpMember p)
            {
                impl = p;
                RefreshData();
            }

            public void RefreshData()
            {
                Text = impl.DllName;
                SubItems.Clear();
                SubItems[0].Text = impl.DllName;
                SubItems.Add(impl.SymbolName);
                switch(impl.ImportNameType)
                {
                    case ImportNameType.INT_KeepUnchanged:
                        SubItems.Add("Unchanged");
                        break;
                    case ImportNameType.INT_NoPrefix:
                        SubItems.Add("NoPrefix");
                        break;
                    case ImportNameType.INT_UnDecorate:
                        SubItems.Add("Undecorate");
                        break;
                    case ImportNameType.INT_Ordinal:
                        SubItems.Add("Ordinal");
                        break;
                }

                SubItems.Add(impl.OrdinalOrHint.ToString());
                if (impl.IsImportByName) {
                    SubItems.Add(impl.ImportName);
                } else {
                    SubItems.Add("");
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "lib";
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;
            ofd.Title = "Open a import library file";
            ofd.Filter = "Import Library|*.lib";

            if (ofd.ShowDialog() == DialogResult.OK) {
                m_fileData = File.ReadAllBytes(ofd.FileName);
                if (m_pinHandle.IsAllocated)
                    m_pinHandle.Free();
                m_pinHandle = GCHandle.Alloc(m_fileData, GCHandleType.Pinned);

                m_lib = CreateImpLibWalker();
                m_lib.SetSource(ref m_fileData[0], m_fileData.Length);
                listView1.BeginUpdate();
                listView1.Items.Clear();

                while(m_lib.MoveNext())
                {
                    IImpMember impmem = m_lib.Current;
                    LVIImportItem item = new LVIImportItem(impmem);
                    listView1.Items.Add(item);
                }
                listView1.EndUpdate();
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            foreach (var x in listView1.Items)
                ((LVIImportItem)x).Selected = true;
            listView1.EndUpdate();
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            foreach (var x in listView1.Items)
                ((LVIImportItem)x).Selected = false;
            listView1.EndUpdate();
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            foreach (var x in listView1.Items)
                ((LVIImportItem)x).Selected = !((LVIImportItem)x).Selected;
            listView1.EndUpdate();
        }

        private void MenuChangeToSeries(object sender, EventArgs e, ImportNameType newType)
        {
            listView1.BeginUpdate();
            foreach (var x in listView1.Items)
            {
                LVIImportItem lvi = x as LVIImportItem;
                if (lvi.Selected)
                {
                    lvi.Impl.ImportNameType = newType;
                    lvi.RefreshData();
                }
            }
            listView1.EndUpdate();
        }

        private void changeToKeepTheSameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuChangeToSeries(sender, e, ImportNameType.INT_KeepUnchanged);
        }

        private void changeToNoPrefixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuChangeToSeries(sender, e, ImportNameType.INT_NoPrefix);
        }

        private void changeToUnderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuChangeToSeries(sender, e, ImportNameType.INT_UnDecorate);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "lib";
            sfd.RestoreDirectory = true;
            sfd.Title = "Save a import library file";
            sfd.Filter = "Import Library|*.lib";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, m_fileData);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Import Library Editor\r\nSoraYuki (LeiMing), 2014", "About");
        }
    }
}
