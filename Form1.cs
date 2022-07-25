using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Editor
{
    public partial class F_TelaInicial : Form
    {
        StringReader leitura = null;
        
        public F_TelaInicial()
        {
            InitializeComponent();
        }

        private void NewDocument()
        {
            richTextBox1.Clear();
            richTextBox1.Focus();
        }

        private void SaveDocument()
        {
            try
            {
                if(this.saveFileDialog1.ShowDialog() == DialogResult.OK) //janela savefiledialog é a janela de salvar como
                {
                    FileStream arquivo = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write); //Construtor(Arquivo vem do nome criado a partir da janela, arquivo criado ou escolhido da janela, vai escrever no arquivo)
                    StreamWriter fr_streamWriter = new StreamWriter(arquivo); //construtor responsavel por escreve no arquivo
                    fr_streamWriter.Flush(); //esvazia o buffer
                    fr_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin); //começa desde o inicio do arquivo
                    fr_streamWriter.Write(this.richTextBox1); //escrever no arquivo o texto que estiver contido na janela criada na interface
                    fr_streamWriter.Flush();
                    fr_streamWriter.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Erro na gravação: " + ex.Message, "Erro ao gravar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDocument()
        {
            this.openFileDialog1.Multiselect = false; //não permite que mais de um documento seja aberto
            this.openFileDialog1.Title = "Open File";
            openFileDialog1.InitialDirectory = @"C:\Users\frede\OneDrive\Documents\Curso CSharp\Visual\Editor\";
            openFileDialog1.Filter = "(*.TXT)|*.TXT";

            DialogResult dr = this.openFileDialog1.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    FileStream arquivo = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read); //mesma função la de cima mas agora é para abertura e não para salvamento do arquivo
                    StreamReader fr_streamReader = new StreamReader(arquivo);
                    fr_streamReader.BaseStream.Seek(0, SeekOrigin.Begin); //começar a leitura a partir do inicio do arquitvo
                    this.richTextBox1.Text = ""; //esvazia a caixa de texto antes de adicionar o arquivo
                    string linha = fr_streamReader.ReadLine();
                    while(linha != null)
                    {
                        this.richTextBox1.Text += linha + "\n";
                        linha = fr_streamReader.ReadLine();
                    }
                    fr_streamReader.Close();
                }catch(Exception ex)
                {
                    MessageBox.Show("Erro na leitura: " + ex.Message, "Erro ao abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Copy()
        {
            if(richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void Paste()
        {
            richTextBox1.Paste();
        }

        private void Bold()
        {
            Font currentFont = richTextBox1.SelectionFont;

            FontStyle newFontStyle = (currentFont.Bold) ? FontStyle.Regular : FontStyle.Bold;

            if (currentFont.Italic) newFontStyle |= FontStyle.Italic;
            if (currentFont.Underline) newFontStyle |= FontStyle.Underline;

            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void Italic()
        {
            Font currentFont = richTextBox1.SelectionFont;

            FontStyle newFontStyle = (currentFont.Italic) ? FontStyle.Regular : FontStyle.Italic;

            if (currentFont.Italic) newFontStyle |= FontStyle.Bold;
            if (currentFont.Underline) newFontStyle |= FontStyle.Underline;

            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }
        private void Underline()
        {
            Font currentFont = richTextBox1.SelectionFont;

            FontStyle newFontStyle = (currentFont.Underline) ? FontStyle.Regular : FontStyle.Underline;

            if (currentFont.Italic) newFontStyle |= FontStyle.Italic;
            if (currentFont.Underline) newFontStyle |= FontStyle.Bold;

            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void leftAlign()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void rightAlign()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void centerAlign()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void printDocument()
        {
            printDialog1.Document = printDocument1; //print dialog so abre a caixa perguntando se a impressão sera confirmada, o print document é quem faz realmente a impressão
            string txt = this.richTextBox1.Text;
            leitura = new StringReader(txt);

            if(printDialog1.ShowDialog() == DialogResult.OK) //se a pessão clicar me ok e não em cancelar, entra no if
            {
                this.printDocument1.Print();
            }
        }

        private void F_TelaInicial_Load(object sender, EventArgs e)
        {

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void negritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bold();
        }

        private void direitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightAlign();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bnt_new_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void btn_paste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void btn_bold_Click(object sender, EventArgs e)
        {
            Bold();
        }

        private void btn_italic_Click(object sender, EventArgs e)
        {
            Italic();
        }

        private void btn_underlined_Click(object sender, EventArgs e)
        {
            Underline();
        }

        private void itálicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Italic();
        }

        private void sublinhadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Underline();
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            leftAlign();
        }

        private void esquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftAlign();
        }

        private void centralizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerAlign();
        }

        private void btn_Center_Click(object sender, EventArgs e)
        {
            centerAlign();
        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            rightAlign();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linhasPage = 0;
            float posY = 0;
            float cont = 0;
            float margemEsquerda = e.MarginBounds.Left - 50;
            float margemSuperior = e.MarginBounds.Top - 50;

            if(margemEsquerda < 5)
            {
                margemEsquerda = 20;
            }

            if(margemSuperior<5)
            {
                margemSuperior = 20;
            }

            string linha = null;
            Font font = this.richTextBox1.Font;
            SolidBrush pincel = new SolidBrush(Color.Black);
            linhasPage = e.PageBounds.Height / font.GetHeight(e.Graphics); // metodo para encontrar a quantidade de linhas por pagina, utilizando como referencia a medida das margens, font.getheight serve para medir o tamanho das letras
            linha = leitura.ReadLine(); //ler as linhas do elemento leitura que recebeu as linhas do richtechbox
            while(cont<linhasPage)
            {
                posY = margemSuperior + (cont * font.GetHeight(e.Graphics));
                e.Graphics.DrawString(linha, font, pincel, margemEsquerda, posY, new StringFormat());
                cont++;
                linha = leitura.ReadLine();
            }
            if(linha != null)
            {
                e.HasMorePages = true; //se estiverem mais linhas, é para imprimir em mais paginas
            }
            else
            {
                e.HasMorePages = false;
            }
            pincel.Dispose();
            
        }
    }
}
