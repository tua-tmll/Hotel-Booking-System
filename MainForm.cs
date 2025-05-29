using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace LibrarySystem
{
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
            
            // Initialize Material Design
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue800,
                Primary.Blue900,
                Primary.Blue500,
                Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form settings
            this.Text = "Library Management System";
            this.Size = new System.Drawing.Size(800, 600);
            
            // Create Material Design controls
            var tabControl = new MaterialTabControl();
            tabControl.Dock = DockStyle.Fill;
            
            // Books tab
            var booksTab = new TabPage("Books");
            var booksPanel = new MaterialCard
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            
            var addBookButton = new MaterialButton
            {
                Text = "Add Book",
                Dock = DockStyle.Top,
                Margin = new Padding(5)
            };
            addBookButton.Click += AddBookButton_Click;
            
            var booksList = new MaterialListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };
            booksList.Columns.Add("Title", 200);
            booksList.Columns.Add("Author", 150);
            booksList.Columns.Add("ISBN", 100);
            booksList.Columns.Add("Status", 100);
            
            booksPanel.Controls.Add(booksList);
            booksPanel.Controls.Add(addBookButton);
            booksTab.Controls.Add(booksPanel);
            
            // Magazines tab
            var magazinesTab = new TabPage("Magazines");
            var magazinesPanel = new MaterialCard
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            
            var addMagazineButton = new MaterialButton
            {
                Text = "Add Magazine",
                Dock = DockStyle.Top,
                Margin = new Padding(5)
            };
            addMagazineButton.Click += AddMagazineButton_Click;
            
            var magazinesList = new MaterialListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };
            magazinesList.Columns.Add("Title", 200);
            magazinesList.Columns.Add("Publisher", 150);
            magazinesList.Columns.Add("Issue", 100);
            magazinesList.Columns.Add("Status", 100);
            
            magazinesPanel.Controls.Add(magazinesList);
            magazinesPanel.Controls.Add(addMagazineButton);
            magazinesTab.Controls.Add(magazinesPanel);
            
            // Add tabs to control
            tabControl.TabPages.Add(booksTab);
            tabControl.TabPages.Add(magazinesTab);
            
            // Add tab control to form
            this.Controls.Add(tabControl);
            
            this.ResumeLayout(false);
        }

        private void AddBookButton_Click(object sender, EventArgs e)
        {
            using (var form = new AddBookForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshBookList();
                }
            }
        }

        private void AddMagazineButton_Click(object sender, EventArgs e)
        {
            using (var form = new AddMagazineForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshMagazineList();
                }
            }
        }

        private void RefreshBookList()
        {
            var booksList = ((MaterialListView)((MaterialCard)((TabPage)((MaterialTabControl)Controls[0]).TabPages[0]).Controls[0]).Controls[1]);
            booksList.Items.Clear();
            
            foreach (var item in LibraryManager.Items)
            {
                if (item is Book book)
                {
                    var listItem = new ListViewItem(new[]
                    {
                        book.Title,
                        book.Author,
                        book.ISBN,
                        book.IsAvailable ? "Available" : "Checked Out"
                    });
                    booksList.Items.Add(listItem);
                }
            }
        }

        private void RefreshMagazineList()
        {
            var magazinesList = ((MaterialListView)((MaterialCard)((TabPage)((MaterialTabControl)Controls[0]).TabPages[1]).Controls[0]).Controls[1]);
            magazinesList.Items.Clear();
            
            foreach (var item in LibraryManager.Items)
            {
                if (item is Magazine magazine)
                {
                    var listItem = new ListViewItem(new[]
                    {
                        magazine.Title,
                        magazine.Publisher,
                        magazine.IssueNumber.ToString(),
                        magazine.IsAvailable ? "Available" : "Checked Out"
                    });
                    magazinesList.Items.Add(listItem);
                }
            }
        }
    }
} 