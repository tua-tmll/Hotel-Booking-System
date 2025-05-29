using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace LibrarySystem
{
    public partial class AddBookForm : MaterialForm
    {
        public AddBookForm()
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
            this.Text = "Add New Book";
            this.Size = new System.Drawing.Size(400, 500);
            
            // Create Material Design controls
            var panel = new MaterialCard
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            
            // Title
            var titleLabel = new MaterialLabel
            {
                Text = "Title:",
                Location = new System.Drawing.Point(10, 20)
            };
            
            var titleTextBox = new MaterialTextBox
            {
                Location = new System.Drawing.Point(10, 50),
                Size = new System.Drawing.Size(360, 30)
            };
            
            // Author
            var authorLabel = new MaterialLabel
            {
                Text = "Author:",
                Location = new System.Drawing.Point(10, 90)
            };
            
            var authorTextBox = new MaterialTextBox
            {
                Location = new System.Drawing.Point(10, 120),
                Size = new System.Drawing.Size(360, 30)
            };
            
            // ISBN
            var isbnLabel = new MaterialLabel
            {
                Text = "ISBN:",
                Location = new System.Drawing.Point(10, 160)
            };
            
            var isbnTextBox = new MaterialTextBox
            {
                Location = new System.Drawing.Point(10, 190),
                Size = new System.Drawing.Size(360, 30)
            };
            
            // Price
            var priceLabel = new MaterialLabel
            {
                Text = "Price:",
                Location = new System.Drawing.Point(10, 230)
            };
            
            var priceTextBox = new MaterialTextBox
            {
                Location = new System.Drawing.Point(10, 260),
                Size = new System.Drawing.Size(360, 30)
            };
            
            // Publisher
            var publisherLabel = new MaterialLabel
            {
                Text = "Publisher:",
                Location = new System.Drawing.Point(10, 300)
            };
            
            var publisherTextBox = new MaterialTextBox
            {
                Location = new System.Drawing.Point(10, 330),
                Size = new System.Drawing.Size(360, 30)
            };
            
            // Edition
            var editionLabel = new MaterialLabel
            {
                Text = "Edition:",
                Location = new System.Drawing.Point(10, 370)
            };
            
            var editionTextBox = new MaterialTextBox
            {
                Location = new System.Drawing.Point(10, 400),
                Size = new System.Drawing.Size(360, 30)
            };
            
            // Add button
            var addButton = new MaterialButton
            {
                Text = "Add Book",
                Location = new System.Drawing.Point(10, 440),
                Size = new System.Drawing.Size(360, 40)
            };
            addButton.Click += (sender, e) =>
            {
                if (decimal.TryParse(priceTextBox.Text, out decimal price) &&
                    int.TryParse(editionTextBox.Text, out int edition))
                {
                    var book = new Book(
                        titleTextBox.Text,
                        Guid.NewGuid().ToString(),
                        price,
                        authorTextBox.Text,
                        isbnTextBox.Text,
                        0, // Page count not implemented in UI
                        publisherTextBox.Text,
                        edition
                    );
                    
                    LibraryManager.AddItem(book);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter valid price and edition numbers.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            
            // Add controls to panel
            panel.Controls.AddRange(new Control[]
            {
                titleLabel, titleTextBox,
                authorLabel, authorTextBox,
                isbnLabel, isbnTextBox,
                priceLabel, priceTextBox,
                publisherLabel, publisherTextBox,
                editionLabel, editionTextBox,
                addButton
            });
            
            // Add panel to form
            this.Controls.Add(panel);
            
            this.ResumeLayout(false);
        }
    }
} 