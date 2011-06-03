using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuestTracker.Data;
using QuestTracker.IO;
using QuestTracker.QuestControls.Properties;

namespace QuestTracker.QuestControls
{
    public partial class QuestLogControl : UserControl
    {
        public QuestLog QuestLog
        {
            get
            {
                return questLog;
            }
            set
            {
                questLog = value;
            }
        }

        public QuestTabControl CurrentTabControl { get { return questTabs.SelectedTab.QuestTabControl(); } }

        public QuestGroupControl LastSelectedQuestGroupControl { get; set; }
        public QuestControl LastSelectedQuestControl { get; set; }
        public QuestGroup LastSelectedQuestGroup { get; set; }
        public Quest LastSelectedQuest { get; set; }

        public event EventHandler SelectionPluralityChanged;
        public event EventHandler QuestSelectionChanged;

        private string currentName;
        private QuestLog questLog;
        private TabPage lastSelectedQuestTabControl;
        private QuestTab lastSelectedQuestTab;

        public QuestTab LastSelectedQuestTab
        {
            get { return lastSelectedQuestTab; }
            set
            {
                lastSelectedQuestTab = value;

                if (value == null)
                    return;

                var lastSelectedQuery = from TabPage questTabPage in questTabs.TabPages
                                        where questTabPage.QuestTabControl().QuestTab == lastSelectedQuestTab
                                        select questTabPage;

                LastSelectedQuestTabControl = lastSelectedQuery.First();
            }
        }

        public TabPage LastSelectedQuestTabControl
        {
            get
            {
                return lastSelectedQuestTabControl;
            }
            set
            {
                lastSelectedQuestTabControl = value;
                lastSelectedQuestTab = lastSelectedQuestTabControl.QuestTabControl().QuestTab;
            }
        }

        public QuestLogControl()
        {
            InitializeComponent();
        }

        public void RenderLog()
        {
            RenderTabs();
            CurrentTabControl.RenderTab(QuestLog.ShowCompletedQuests);
        }

        private void RenderTabs()
        {
            //remove tabs that aren't in the log anymore
            var controlsToDelete = (from Control questTabPage in questTabs.Controls.Cast<Control>()
                                    where questTabPage is TabPage && ((TabPage)questTabPage).QuestTabControl() != null && !QuestLog.Tabs.Contains(((TabPage)questTabPage).QuestTabControl().QuestTab)
                                    select (TabPage)questTabPage).ToList();

            foreach (var questTabPage in controlsToDelete)
            {
                questTabs.TabPages.Remove(questTabPage);
            }

            //add tabs that are in the log
            var questTabsInControls = from Control questTabPage in questTabs.Controls.Cast<Control>()
                                      where questTabPage is TabPage && ((TabPage)questTabPage).QuestTabControl() != null
                                      select ((TabPage)questTabPage).QuestTabControl().QuestTab;

            var questTabsToAdd = from QuestTab questTab in QuestLog.Tabs
                                 where !questTabsInControls.Contains(questTab)
                                 select questTab;

            foreach (var questTab in questTabsToAdd)
            {
                var newTab = new TabPage(questTab.Name);
                var newQuestTabControl = new QuestTabControl { Dock = DockStyle.Fill, QuestTab = questTab };

                newQuestTabControl.QuestSelectionChanged += questTabControl_QuestSelectionChanged;
                newQuestTabControl.SelectionPluralityChanged += questTabControl_SelectionPluralityChanged;

                newTab.Controls.Add(newQuestTabControl);
                newQuestTabControl.RenderTab();
                questTabs.TabPages.Insert(questTabs.TabPages.Count - 1, newTab);
            }

            if (questTabs.SelectedTab == addTab)
            {
                questTabs.SelectedTab = questTabs.TabPages[0];
                LastSelectedQuestTabControl = questTabs.SelectedTab;
            }
        }

        public void RenderLog(bool showCompleted)
        {
            RenderTabs();
            CurrentTabControl.RenderTab(showCompleted);
        }

        private void questTabs_Click(object sender, EventArgs e)
        {
            if (GetTabPageByTab(PointToClient(MousePosition)) == addTab)
            {
                AddNewTab();
                questTabs_DoubleClick(sender, e);
            }
            else
            {
                LastSelectedQuestTab = CurrentTabControl.QuestTab;
            }
        }

        private void AddNewTab()
        {
            var newQuestTab = new QuestTab { Name = "New Tab" };

            QuestLog.Tabs.Add(newQuestTab);

            RenderLog();

            LastSelectedQuestTab = newQuestTab;

            if (LastSelectedQuestTabControl != null)
            {
                questTabs.SelectedTab = LastSelectedQuestTabControl;
                questTabs_DoubleClick(this, EventArgs.Empty);
            }
        }

        private void questTabs_DoubleClick(object sender, EventArgs e)
        {
            rename.Width = questTabs.GetTabRect(questTabs.SelectedIndex).Width - 2;
            rename.Left = questTabs.GetTabRect(questTabs.SelectedIndex).Left + 3;
            rename.Text = questTabs.SelectedTab.Text;
            currentName = questTabs.SelectedTab.Text;
            rename.Visible = true;
            rename.SelectAll();
            rename.Focus();
        }

        private void rename_TextChanged(object sender, EventArgs e)
        {
            questTabs.SelectedTab.Text = rename.Text.Trim().Replace("\r", "").Replace("\n", "");
            CurrentTabControl.QuestTab.Name = questTabs.SelectedTab.Text;
            rename.Width = questTabs.GetTabRect(questTabs.SelectedIndex).Width - 2;
        }

        private void rename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            rename.SelectionLength = 0;
            rename.Visible = false;
            questTabs.SelectedTab.Focus();
        }

        private void rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;

            rename.Text = currentName;
            rename.Visible = false;
            questTabs.SelectedTab.Focus();
        }

        private void rename_Leave(object sender, EventArgs e)
        {
            rename.Visible = false;
        }

        private void questTabControl_QuestSelectionChanged(object sender, EventArgs e)
        {
            if (QuestSelectionChanged != null)
                QuestSelectionChanged(this, e);
        }

        private void questTabControl_SelectionPluralityChanged(object sender, EventArgs e)
        {
            if (SelectionPluralityChanged != null)
                SelectionPluralityChanged(this, e);
        }

        public void CompleteQuests()
        {
            CurrentTabControl.CompleteQuests();
        }

        public void DeleteQuests()
        {
            if (MessageBox.Show(Resources.DeleteQuestsDialogText, Resources.DeleteQuestsDialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            var currentTabControl = CurrentTabControl;

            if (currentTabControl.AnyChecked || currentTabControl.LastSelectedQuest != null)
            {
                FileWriter.Export(QuestLog);

                currentTabControl.DeleteQuests();
            }
        }

        public void SelectLastSelected()
        {
            CurrentTabControl.SelectLastSelected();
        }

        public static QuestLogControl GetQuestLog(Control control)
        {
            var retVal = control.Parent;

            if (retVal == null) throw new Exception("Could not identify quest log.");

            while (!(retVal is QuestLogControl) && retVal.Parent != null)
            {
                retVal = retVal.Parent;
            }

            if (!(retVal is QuestLogControl))
                return null;

            return (QuestLogControl)retVal;
        }

        private void questTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentTabControl = CurrentTabControl;

            if (currentTabControl != null && currentTabControl.QuestTab != null)
            {
                currentTabControl.RenderTab(questLog.ShowCompletedQuests);
                currentTabControl.ChangeSelectedQuest();
                currentTabControl.SelectLastSelected();
            }

            if (questTabs.SelectedTab == addTab)
            {
                questTabs.SelectedIndex = 0;
            }
        }

        private void removeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCurrentTab();
        }

        public void RemoveCurrentTab()
        {
            if (MessageBox.Show(Resources.RemoveTabDialogText, Resources.RemoveTabDialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            FileWriter.Export(questLog);
            questLog.Tabs.Remove(CurrentTabControl.QuestTab);
            RenderLog();
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            if (questTabs.GetTabRect(questTabs.SelectedIndex).Contains(PointToClient(MousePosition)))
                e.Cancel = false;
            else
            {
                for (int i = 0; i < questTabs.TabPages.Count - 1; i++)
                {
                    if (!questTabs.GetTabRect(i).Contains(PointToClient(MousePosition))) continue;

                    questTabs.SelectedIndex = i;
                    e.Cancel = false;
                    break;
                }
            }
        }

        private void questTabs_DragOver(object sender, DragEventArgs e)
        {
            var pt = new Point(e.X, e.Y);
            pt = PointToClient(pt);

            var hoverTab = GetTabPageByTab(pt);
            
            if (hoverTab != null && hoverTab != addTab)
            {
                if (e.Data.GetDataPresent(typeof(TabPage)))
                {
                    e.Effect = DragDropEffects.Move;
                    var dragTab = (TabPage)e.Data.GetData(typeof(TabPage));
                    var itemDragIndex = FindIndex(dragTab);
                    var dropLocationIndex = FindIndex(hoverTab);

                    if (itemDragIndex != dropLocationIndex)
                    {
                        if (FarHalfOfTab(dropLocationIndex, itemDragIndex))
                        {
                            var pages = new ArrayList();
                            for (int i = 0; i < questTabs.TabPages.Count; i++)
                            {
                                if (i != itemDragIndex)
                                    pages.Add(questTabs.TabPages[i]);
                            }

                            pages.Insert(dropLocationIndex, dragTab);
                            SwapQuestTabs(dropLocationIndex, itemDragIndex);
                            questTabs.TabPages.Clear();
                            questTabs.TabPages.AddRange((TabPage[]) pages.ToArray(typeof(TabPage)));
                            questTabs.SelectedTab = dragTab;
                        }
                    }
                }

                if (e.Data.GetDataPresent(typeof(QuestGroupControl)) || e.Data.GetDataPresent(typeof(QuestControl)))
                {
                    questTabs.SelectedTab = hoverTab;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SwapQuestTabs(int dropLocationIndex, int itemDragIndex)
        {
            var tempTab = questLog.Tabs[dropLocationIndex];
            questLog.Tabs[dropLocationIndex] = questLog.Tabs[itemDragIndex];
            questLog.Tabs[itemDragIndex] = tempTab;
        }

        private bool FarHalfOfTab(int dropLocationIndex, int itemDragIndex)
        {
            var dropRect = questTabs.GetTabRect(dropLocationIndex);
            var dropTabHalfWidth = dropRect.Width / 2;

            if (dropLocationIndex > itemDragIndex)
            {
                //coming from left
                var point = new Point(MousePosition.X - dropTabHalfWidth, MousePosition.Y);
                point = PointToClient(point);

                if (FindIndex(point) == dropLocationIndex)
                    return true;
            }
            else
            {
                //coming from right
                var point = new Point(MousePosition.X + dropTabHalfWidth, MousePosition.Y);
                point = PointToClient(point);

                if (FindIndex(point) == dropLocationIndex)
                    return true;
            }

            return false;
        }

        private TabPage GetTabPageByTab(Point pt)
        {
            TabPage tp = null;

            for (int i = 0; i < questTabs.TabPages.Count; i++)
            {
                if (questTabs.GetTabRect(i).Contains(pt))
                {
                    tp = questTabs.TabPages[i];
                    break;
                }
            }

            return tp;
        }

        private int FindIndex(TabPage page)
        {
            for (int i = 0; i < questTabs.TabPages.Count; i++)
            {
                if (questTabs.TabPages[i] == page)
                    return i;
            }

            return -1;
        }

        private int FindIndex(Point point)
        {
            for (int i = 0; i < questTabs.TabPages.Count; i++)
            {
                if (questTabs.GetTabRect(i).Contains(point))
                {
                    return i;
                }
            }

            return -1;
        }


        private void questTabs_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons != MouseButtons.Left)
                return;

            var point = new Point(e.X, e.Y);
            var hoverTab = GetTabPageByTab(point);

            if (hoverTab != null && hoverTab != addTab)
            {
                DoDragDrop(hoverTab, DragDropEffects.All);
            }
        }
    }
}
