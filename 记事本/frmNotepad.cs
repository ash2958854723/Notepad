using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 记事本
{///
    ///

    public partial class frmNotepad : Form
    {///
        /* 布尔变量b用于判断文件是新建的还是从磁盘打开的，
    true表示文件是从磁盘打开的，false表示文件是新建的，默认值为false*/
        bool b = false;
        /* 布尔变量s用于判断文件件是否被保存，
           true表示文件是已经被保存了，false表示文件未被保存，默认值为true*/
      //  bool s = true;



        bool isChange = false;//申请一个布尔类型的变量来判断文件是否保存
        string filePath = "";//变量判断文件是新建的还是从磁盘打开的


      
        int rowNum;
        int columnNum;
        public frmNotepad()
        {
            InitializeComponent();
        }
        private bool SaveAs()//设置一个返回bool值的方法
        {
            DialogResult dr = SaveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                richTextBox1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                isChange = true;
                return true;//如果储存成功 则返回 true
            }
            else
            {
                return false;//如果储存失败 则返回 false
            }
        }

        private void GetLineColumn()
        {
            rowNum = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
            columnNum = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine()+1;
            toolStripStatusLabel1.Text = "行" + rowNum.ToString() + "列" + columnNum.ToString();
        }


        private void Exit()
        {
            //判断当前内容是否更改 
            if (isChange == true)
            {
                DialogResult dr = MessageBox.Show("是否保存已更改文件", "另存为", MessageBoxButtons.YesNoCancel);
           
                switch (dr)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        Application.Exit();
                        break;
                    case DialogResult.Yes:
                        //用户点击是 后 判断是否存在储存路径
                        if (filePath == "")
                        {
                            
                            //当用户点击另存后打开界面后取消 则不关闭 
                            if (SaveAs())
                                Application.Exit();
                            
                        }
                        else
                        {
                           Save();
                            this.Close();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Save()
        {
            {
                //  若文件从磁盘打开并且修改了其内容
                if (b == true && richTextBox1.Modified == true)
                {
                    richTextBox1.SaveFile(openFileDialog1.FileName);
                    isChange = true;
                }
                else if (b == false && richTextBox1.Text.Trim() != "" &&
                    SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(SaveFileDialog1.FileName);
                    isChange = true;
                    b = true;
                    openFileDialog1.FileName = SaveFileDialog1.FileName;
                }
            }

        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isChange = true;


        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            if (isChange == true || richTextBox1.Text.Trim() != "")
            {
                if (isChange == true)//如果文件没有保存
                {
                    string result;
                    result = MessageBox.Show("文件尚未保存，是否保存?", "保存文件", MessageBoxButtons.YesNoCancel).ToString();//弹出对话框，显示是否保存
                    switch (result)
                    {
                        case "Yes":
                            if (isChange == true)
                            {
                                richTextBox1.SaveFile(openFileDialog1.FileName);
                            }
                            else if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                richTextBox1.SaveFile(SaveFileDialog1.FileName);//直接使用saveFileDialog保存文件
                            }
                            isChange = false;//表示文件已保存
                            richTextBox1.Text = "";//记事本内容清空；
                            break;
                        case "No":
                            isChange = false;
                            richTextBox1.Text = "";
                            break;
                    }
                }
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            {
               //  若文件从磁盘打开并且修改了其内容
                if (b == true && richTextBox1.Modified == true)
                {
                    richTextBox1.SaveFile(openFileDialog1.FileName);
                    isChange = true;
                }
                else if (b == false && richTextBox1.Text.Trim() != "" &&
                    SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(SaveFileDialog1.FileName);
                    isChange = true;
                    b = true;
                    openFileDialog1.FileName = SaveFileDialog1.FileName;
                }
            }

        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Exit();
            /*  //如果输入框的内容在退出时不保存，就清空并且退出
              else
              {
                  richTextBox1.Clear();

              }
              this.Close();
          }

         */



























        }

        private void tsmiUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void tsmiFind_Click(object sender, EventArgs e)
        {
            Find F = new Find();
            F.Show(this);
            F.TopLevel = true;
        }

        private void tsmiAlter_Click(object sender, EventArgs e)
        {

        }

        private void tsmiGoto_Click(object sender, EventArgs e)
        {

        }

        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(System.DateTime.Now.ToString());
        }






        private void tsmiLineWrap_Click(object sender, EventArgs e)
        {
            if (tsmiLineWrap.Checked == false)
            {
                tsmiLineWrap.Checked = true;        	// 选中该菜单项
                richTextBox1.WordWrap = true;       	// 设置为自动换行
            }
            else
            {
                tsmiLineWrap.Checked = false;
                richTextBox1.WordWrap = false;
            }
        }




        private void tsmiFont_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = fontDialog1.Color;
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }







        private void tsmiStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void tsmiViewHelp_Click(object sender, EventArgs e)
        {

        }






        private void tsmiAbout_Click(object sender, EventArgs e)
        {

        }

        private void frmNotepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isChange != true && richTextBox1.Text.Trim() != "")
                Application.Exit();
            
                if (isChange == true)//当文本内容发生改变时 
                {
                    DialogResult dr = MessageBox.Show("文件尚未保存，是否保存?", "保存文件", MessageBoxButtons.YesNoCancel);//弹出对话框，显示是否保存,获取用户的选择
                    switch (dr)
                    {

                        case DialogResult.Cancel://当用户选择 取消 时发生的事件
                            e.Cancel = true;//将 FromClosing 事件的取消属性 置为 真（触发该事件的取消属性）
                            break;

                        case DialogResult.No:
                            break;

                        case DialogResult.Yes:
                            if(filePath  == "")
                            {
                                if (!SaveAs())//如果储存失败，则 !false = true （也就是说！SaveAs（）为 true）
                                {
                                    e.Cancel = true;//将 FromClosing 事件的取消属性 置为 真（触发该事件的取消属性）
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    /*switch (result)
                    {
                        case "Yes":
                            if (isChange == true)
                            {
                                richTextBox1.SaveFile(openFileDialog1.FileName);
                            }
                            else if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                richTextBox1.SaveFile(SaveFileDialog1.FileName);//直接使用saveFileDialog保存文件
                            }
                            isChange = false;//表示文件已保存
                            richTextBox1.Text = "";//记事本内容清空；
                            break;
                        case "No":
                            isChange = false;
                            richTextBox1.Text = "";
                            this.Close();
                            break;*/



                



                    
                
               
            }

        }

        private void tsmiEditor_Click(object sender, EventArgs e)
        {
           
            if (richTextBox1.SelectedText != "")
            {
              tsmiCopy.Enabled = true;
            }
            if (richTextBox1.Text == "")
            {
                tsmiFind.Enabled = false;

            }
            else
            {
                tsmiFind.Enabled = true;
            }
        }

        private void tsmiColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = colorDialog1.Color;
        }

      

       

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            GetLineColumn();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            GetLineColumn();
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            GetLineColumn();
        }

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            GetLineColumn();
        }

       
        
        









          
    }
}
