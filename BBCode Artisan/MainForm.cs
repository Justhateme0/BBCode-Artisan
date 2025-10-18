namespace BBCode_Artisan
{
    public partial class MainForm : Form
    {
        private readonly BBCodeConverter _converter;
        private readonly ProfileManager _profileManager;
        private CustomTitleBar _titleBar = null!;
        private bool _isDarkTheme = true;

        private Panel leftPanel = null!;
        private Panel centerPanel = null!;
        private Panel rightPanel = null!;

        private Button btnBold = null!;
        private Button btnItalic = null!;
        private Button btnUnderline = null!;
        private Button btnStrikethrough = null!;
        private Button btnUrl = null!;
        private Button btnImage = null!;
        private Button btnCode = null!;
        private Button btnQuote = null!;
        private Button btnList = null!;
        private Button btnOrderedList = null!;

        private ComboBox cmbFontFamily = null!;
        private NumericUpDown numFontSize = null!;
        private Button btnTextColor = null!;
        private Button btnBgColor = null!;

        private TextBox txtUrl = null!;
        private NumericUpDown numImageWidth = null!;
        private NumericUpDown numImageHeight = null!;

        private CheckBox chkReplaceNewlines = null!;

        private ComboBox cmbProfiles = null!;
        private Button btnSaveProfile = null!;
        private Button btnDeleteProfile = null!;

        private RichTextBox txtSource = null!;
        private WebBrowser webPreview = null!;
        private RichTextBox txtBBCode = null!;

        private Button btnCopy = null!;
        private Button btnExport = null!;

        private Profile _currentProfile = null!;

        public MainForm()
        {
            _converter = new BBCodeConverter();
            _profileManager = new ProfileManager();

            InitializeComponent();
            InitializeCustomComponents();
            LoadProfiles();
            ApplyProfile(_currentProfile);
            ApplyTheme();
        }

        private void InitializeCustomComponents()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1300, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 20);

            _titleBar = new CustomTitleBar(this);
            _titleBar.ThemeToggleClicked += (s, e) => ToggleTheme();
            this.Controls.Add(_titleBar);

            var mainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Padding = new Padding(0, 40, 0, 0),
                BackColor = Color.FromArgb(20, 20, 20)
            };

            mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260));
            mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));

            leftPanel = CreateLeftPanel();
            centerPanel = CreateCenterPanel();
            rightPanel = CreateRightPanel();

            mainContainer.Controls.Add(leftPanel, 0, 0);
            mainContainer.Controls.Add(centerPanel, 1, 0);
            mainContainer.Controls.Add(rightPanel, 2, 0);

            this.Controls.Add(mainContainer);
        }

        private Panel CreateLeftPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(25, 25, 25),
                Padding = new Padding(10, 5, 10, 5)
            };

            var container = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(0)
            };

            var lblQuickTags = CreateSectionLabel("QUICK TAGS");
            container.Controls.Add(lblQuickTags);

            var tagsPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                MaximumSize = new Size(240, 0),
                Margin = new Padding(0, 0, 0, 8)
            };

            btnBold = CreateIconButton("B", "Bold");
            btnItalic = CreateIconButton("I", "Italic");
            btnUnderline = CreateIconButton("U", "Underline");
            btnStrikethrough = CreateIconButton("S", "Strike");
            btnUrl = CreateIconButton("🔗", "Link");
            btnImage = CreateIconButton("🖼", "Image");
            btnCode = CreateIconButton("</>", "Code");
            btnQuote = CreateIconButton("❝❞", "Quote");
            btnList = CreateIconButton("•", "List");
            btnOrderedList = CreateIconButton("1.", "Ordered");

            tagsPanel.Controls.AddRange(new Control[] {
                btnBold, btnItalic, btnUnderline, btnStrikethrough,
                btnUrl, btnImage, btnCode, btnQuote, btnList, btnOrderedList
            });
            container.Controls.Add(tagsPanel);

            container.Controls.Add(CreateSectionLabel("FONT"));

            cmbFontFamily = CreateComboBox(new[] { "Arial", "Times New Roman", "Courier New", "Verdana", "Georgia", "Comic Sans MS" });
            cmbFontFamily.SelectedIndexChanged += Settings_Changed;
            container.Controls.Add(cmbFontFamily);

            var fontSizePanel = CreateLabeledControl("Size:", out numFontSize);
            numFontSize.Minimum = 8;
            numFontSize.Maximum = 72;
            numFontSize.Value = 12;
            numFontSize.ValueChanged += Settings_Changed;
            container.Controls.Add(fontSizePanel);

            btnTextColor = CreateColorButton("Text Color", Color.White);
            btnTextColor.Click += BtnTextColor_Click;
            container.Controls.Add(btnTextColor);

            btnBgColor = CreateColorButton("BG Color", Color.Black);
            btnBgColor.Click += BtnBgColor_Click;
            container.Controls.Add(btnBgColor);

            container.Controls.Add(CreateSectionLabel("LINK"));
            txtUrl = CreateTextBox("https://example.com");
            container.Controls.Add(txtUrl);

            container.Controls.Add(CreateSectionLabel("IMAGE"));

            var imgWidthPanel = CreateLabeledControl("W:", out numImageWidth);
            numImageWidth.Maximum = 2000;
            container.Controls.Add(imgWidthPanel);

            var imgHeightPanel = CreateLabeledControl("H:", out numImageHeight);
            numImageHeight.Maximum = 2000;
            container.Controls.Add(imgHeightPanel);

            container.Controls.Add(CreateSectionLabel("OPTIONS"));
            chkReplaceNewlines = CreateCheckBox("Replace \\n with [br]", true);
            chkReplaceNewlines.CheckedChanged += Settings_Changed;
            container.Controls.Add(chkReplaceNewlines);

            container.Controls.Add(CreateSectionLabel("PROFILES"));
            cmbProfiles = CreateComboBox(Array.Empty<string>());
            cmbProfiles.SelectedIndexChanged += CmbProfiles_SelectedIndexChanged;
            container.Controls.Add(cmbProfiles);

            var profileButtonsPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                MaximumSize = new Size(240, 0),
                Margin = new Padding(0, 2, 0, 5)
            };

            btnSaveProfile = CreateSmallButton("Save");
            btnSaveProfile.Click += BtnSaveProfile_Click;
            btnDeleteProfile = CreateSmallButton("Delete");
            btnDeleteProfile.Click += BtnDeleteProfile_Click;

            profileButtonsPanel.Controls.Add(btnSaveProfile);
            profileButtonsPanel.Controls.Add(btnDeleteProfile);
            container.Controls.Add(profileButtonsPanel);

            panel.Controls.Add(container);
            return panel;
        }

        private Panel CreateCenterPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(20, 20, 20),
                Padding = new Padding(5, 5, 5, 5)
            };

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };

            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            var lblSource = CreateSectionLabel("SOURCE TEXT");
            lblSource.Margin = new Padding(0);

            txtSource = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9.5F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.FromArgb(220, 220, 220),
                Margin = new Padding(0, 0, 0, 5)
            };
            txtSource.TextChanged += TxtSource_TextChanged;

            var lblPreview = CreateSectionLabel("PREVIEW");
            lblPreview.Margin = new Padding(0);

            webPreview = new WebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true,
                Margin = new Padding(0)
            };

            container.Controls.Add(lblSource, 0, 0);
            container.Controls.Add(txtSource, 0, 1);
            container.Controls.Add(lblPreview, 0, 2);
            container.Controls.Add(webPreview, 0, 3);

            panel.Controls.Add(container);
            return panel;
        }

        private Panel CreateRightPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(20, 20, 20),
                Padding = new Padding(0, 5, 5, 5)
            };

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };

            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            var lblBBCode = CreateSectionLabel("BBCODE OUTPUT");
            lblBBCode.Margin = new Padding(0);

            txtBBCode = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9.5F),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.FromArgb(220, 220, 220),
                Margin = new Padding(0, 0, 0, 5)
            };

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0)
            };

            btnCopy = CreateActionButton("Copy", Color.FromArgb(45, 85, 180));
            btnCopy.Click += BtnCopy_Click;
            btnExport = CreateActionButton("Export", Color.FromArgb(60, 120, 70));
            btnExport.Click += BtnExport_Click;

            buttonsPanel.Controls.Add(btnCopy);
            buttonsPanel.Controls.Add(btnExport);

            container.Controls.Add(lblBBCode, 0, 0);
            container.Controls.Add(txtBBCode, 0, 1);
            container.Controls.Add(buttonsPanel, 0, 2);

            panel.Controls.Add(container);
            return panel;
        }

        private Label CreateSectionLabel(string text)
        {
            return new Label
            {
                Text = text,
                ForeColor = Color.FromArgb(120, 150, 255),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 4)
            };
        }

        private Button CreateIconButton(string text, string tooltip)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(46, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(1),
                Cursor = Cursors.Hand,
                Tag = tooltip
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btn.Click += TagButton_Click;
            return btn;
        }

        private ComboBox CreateComboBox(string[] items)
        {
            var cmb = new ComboBox
            {
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(0, 0, 0, 8)
            };
            if (items.Length > 0)
            {
                cmb.Items.AddRange(items);
                cmb.SelectedIndex = 0;
            }
            return cmb;
        }

        private Panel CreateLabeledControl(string label, out NumericUpDown numeric)
        {
            var panel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8)
            };

            var lbl = new Label
            {
                Text = label,
                ForeColor = Color.FromArgb(180, 180, 180),
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 4, 8, 0)
            };

            numeric = new NumericUpDown
            {
                Width = 190,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9F)
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(numeric);
            return panel;
        }

        private Button CreateColorButton(string text, Color color)
        {
            var btn = new Button
            {
                Text = $"{text}: {ColorTranslator.ToHtml(color)}",
                Width = 240,
                Height = 28,
                BackColor = color,
                ForeColor = GetContrastColor(color),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 8),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0)
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            return btn;
        }

        private TextBox CreateTextBox(string placeholder)
        {
            return new TextBox
            {
                Width = 240,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(0, 0, 0, 8),
                PlaceholderText = placeholder
            };
        }

        private CheckBox CreateCheckBox(string text, bool isChecked)
        {
            return new CheckBox
            {
                Text = text,
                ForeColor = Color.FromArgb(180, 180, 180),
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Checked = isChecked,
                Margin = new Padding(0, 0, 0, 8)
            };
        }

        private Button CreateSmallButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(115, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 5, 0)
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 70);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 70);
            return btn;
        }

        private Button CreateActionButton(string text, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(120, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 8, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            var hoverColor = Color.FromArgb(
                Math.Min(color.R + 20, 255),
                Math.Min(color.G + 20, 255),
                Math.Min(color.B + 20, 255)
            );
            btn.FlatAppearance.MouseOverBackColor = hoverColor;
            return btn;
        }

        private void TagButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not string tag)
                return;

            var selStart = txtSource.SelectionStart;
            var selLength = txtSource.SelectionLength;
            var tagName = tag.ToLower();

            switch (tagName)
            {
                case "bold":
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "b");
                    break;
                case "italic":
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "i");
                    break;
                case "underline":
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "u");
                    break;
                case "strike":
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "s");
                    break;
                case "link":
                    var url = string.IsNullOrEmpty(txtUrl.Text) ? null : txtUrl.Text;
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "url", url);
                    break;
                case "image":
                    if (numImageWidth.Value > 0 && numImageHeight.Value > 0)
                    {
                        var imgSize = $"{numImageWidth.Value}x{numImageHeight.Value}";
                        txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "img", imgSize);
                    }
                    else
                    {
                        txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "img");
                    }
                    break;
                case "code":
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "code");
                    break;
                case "quote":
                    txtSource.Text = _converter.InsertTag(txtSource.Text, selStart, selLength, "quote");
                    break;
                case "list":
                    InsertList(false);
                    break;
                case "ordered":
                    InsertList(true);
                    break;
            }

            txtSource.Focus();
            txtSource.SelectionStart = selStart + (tagName == "list" || tagName == "ordered" ? 6 : 3);
            txtSource.SelectionLength = 0;
        }

        private void InsertList(bool ordered)
        {
            var selStart = txtSource.SelectionStart;
            var selLength = txtSource.SelectionLength;
            var listTag = ordered ? "[list=1]\n[*]Item 1\n[*]Item 2\n[/list]" : "[list]\n[*]Item 1\n[*]Item 2\n[/list]";

            if (selLength > 0)
            {
                var selected = txtSource.Text.Substring(selStart, selLength);
                var lines = selected.Split('\n');
                var listItems = string.Join("\n", lines.Select(l => $"[*]{l.Trim()}"));
                var wrappedList = ordered ? $"[list=1]\n{listItems}\n[/list]" : $"[list]\n{listItems}\n[/list]";
                txtSource.Text = txtSource.Text.Remove(selStart, selLength).Insert(selStart, wrappedList);
            }
            else
            {
                txtSource.Text = txtSource.Text.Insert(selStart, listTag);
            }
        }

        private void BtnTextColor_Click(object? sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnTextColor.BackColor = colorDialog.Color;
                var colorHtml = ColorTranslator.ToHtml(colorDialog.Color);
                btnTextColor.Text = $"Text Color: {colorHtml}";
                btnTextColor.ForeColor = GetContrastColor(colorDialog.Color);
                Settings_Changed(sender, e);
            }
        }

        private void BtnBgColor_Click(object? sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBgColor.BackColor = colorDialog.Color;
                var colorHtml = ColorTranslator.ToHtml(colorDialog.Color);
                btnBgColor.Text = $"BG Color: {colorHtml}";
                btnBgColor.ForeColor = GetContrastColor(colorDialog.Color);
                Settings_Changed(sender, e);
            }
        }

        private Color GetContrastColor(Color color)
        {
            var brightness = (color.R * 299 + color.G * 587 + color.B * 114) / 1000;
            return brightness > 128 ? Color.Black : Color.White;
        }

        private void TxtSource_TextChanged(object? sender, EventArgs e)
        {
            UpdateBBCodeAndPreview();
        }

        private void Settings_Changed(object? sender, EventArgs e)
        {
            UpdateBBCodeAndPreview();
        }

        private void UpdateBBCodeAndPreview()
        {
            var bbCode = GenerateBBCode(txtSource.Text);
            txtBBCode.Text = bbCode;

            var html = _converter.ConvertToHtml(bbCode);
            var htmlDoc = _converter.GenerateHtmlPreview(html, _isDarkTheme);
            webPreview.DocumentText = htmlDoc;
        }

        private string GenerateBBCode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var result = text;

            if (cmbFontFamily.SelectedItem?.ToString() != "Arial")
            {
                result = _converter.WrapText(result, "font", cmbFontFamily.SelectedItem?.ToString());
            }

            if (numFontSize.Value != 12)
            {
                result = _converter.WrapText(result, "size", numFontSize.Value.ToString());
            }

            var textColorHtml = btnTextColor.Text.Replace("Text Color: ", "");
            if (textColorHtml != "#FFFFFF" && textColorHtml != "#000000")
            {
                result = _converter.WrapText(result, "color", textColorHtml);
            }

            var bgColorHtml = btnBgColor.Text.Replace("BG Color: ", "");
            if (bgColorHtml != "#000000" && bgColorHtml != "#FFFFFF")
            {
                result = _converter.WrapText(result, "bgcolor", bgColorHtml);
            }

            if (!chkReplaceNewlines.Checked)
            {
                result = result.Replace("\n", "[br]");
            }

            return result;
        }

        private void BtnCopy_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBBCode.Text))
            {
                Clipboard.SetText(txtBBCode.Text);
                MessageBox.Show("BBCode copied to clipboard!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|BBCode files (*.bb)|*.bb|All files (*.*)|*.*",
                DefaultExt = "txt",
                FileName = "bbcode_output.txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, txtBBCode.Text);
                MessageBox.Show($"BBCode exported to {saveDialog.FileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadProfiles()
        {
            var profiles = _profileManager.LoadProfiles();
            cmbProfiles.Items.Clear();

            foreach (var profile in profiles)
            {
                cmbProfiles.Items.Add(profile.Name);
            }

            if (cmbProfiles.Items.Count > 0)
            {
                cmbProfiles.SelectedIndex = 0;
            }

            _currentProfile = profiles.FirstOrDefault() ?? new Profile();
        }

        private void CmbProfiles_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbProfiles.SelectedItem is string profileName)
            {
                var profile = _profileManager.GetProfile(profileName);
                if (profile != null)
                {
                    ApplyProfile(profile);
                    _currentProfile = profile;
                }
            }
        }

        private void ApplyProfile(Profile profile)
        {
            cmbFontFamily.SelectedItem = profile.FontFamily;
            numFontSize.Value = profile.FontSize;

            var textColor = ColorTranslator.FromHtml(profile.TextColor);
            btnTextColor.BackColor = textColor;
            btnTextColor.Text = $"Text Color: {profile.TextColor}";
            btnTextColor.ForeColor = GetContrastColor(textColor);

            var bgColor = ColorTranslator.FromHtml(profile.BackgroundColor);
            btnBgColor.BackColor = bgColor;
            btnBgColor.Text = $"BG Color: {profile.BackgroundColor}";
            btnBgColor.ForeColor = GetContrastColor(bgColor);

            numImageWidth.Value = profile.DefaultImageWidth;
            numImageHeight.Value = profile.DefaultImageHeight;
            chkReplaceNewlines.Checked = profile.ReplaceNewlinesWithBr;
            txtUrl.Text = profile.DefaultUrl;

            UpdateBBCodeAndPreview();
        }

        private Profile GetCurrentProfileSettings()
        {
            return new Profile
            {
                Name = _currentProfile.Name,
                FontFamily = cmbFontFamily.SelectedItem?.ToString() ?? "Arial",
                FontSize = (int)numFontSize.Value,
                TextColor = btnTextColor.Text.Replace("Text Color: ", ""),
                BackgroundColor = btnBgColor.Text.Replace("BG Color: ", ""),
                DefaultImageWidth = (int)numImageWidth.Value,
                DefaultImageHeight = (int)numImageHeight.Value,
                ReplaceNewlinesWithBr = chkReplaceNewlines.Checked,
                DefaultUrl = txtUrl.Text
            };
        }

        private void BtnSaveProfile_Click(object? sender, EventArgs e)
        {
            using var inputDialog = new Form
            {
                Text = "Save Profile",
                Size = new Size(320, 160),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            var label = new Label
            {
                Text = "Profile Name:",
                Left = 15,
                Top = 20,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F)
            };

            var textBox = new TextBox
            {
                Left = 15,
                Top = 45,
                Width = 280,
                Text = _currentProfile.Name,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var btnOk = new Button
            {
                Text = "Save",
                Left = 120,
                Top = 80,
                Width = 80,
                Height = 32,
                DialogResult = DialogResult.OK,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 85, 180),
                ForeColor = Color.White
            };
            btnOk.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Left = 210,
                Top = 80,
                Width = 80,
                Height = 32,
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            inputDialog.Controls.AddRange(new Control[] { label, textBox, btnOk, btnCancel });
            inputDialog.AcceptButton = btnOk;
            inputDialog.CancelButton = btnCancel;

            if (inputDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                var profile = GetCurrentProfileSettings();
                profile.Name = textBox.Text;
                _profileManager.SaveProfile(profile);
                LoadProfiles();
                cmbProfiles.SelectedItem = profile.Name;
                MessageBox.Show($"Profile '{profile.Name}' saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDeleteProfile_Click(object? sender, EventArgs e)
        {
            if (cmbProfiles.SelectedItem is string profileName)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete profile '{profileName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    _profileManager.DeleteProfile(profileName);
                    LoadProfiles();
                    MessageBox.Show($"Profile '{profileName}' deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ToggleTheme()
        {
            _isDarkTheme = !_isDarkTheme;
            ApplyTheme();
            UpdateBBCodeAndPreview();
        }

        private void ApplyTheme()
        {
            _titleBar.UpdateTheme(_isDarkTheme);

            var bgMain = _isDarkTheme ? Color.FromArgb(20, 20, 20) : Color.FromArgb(245, 245, 245);
            var bgPanel = _isDarkTheme ? Color.FromArgb(25, 25, 25) : Color.FromArgb(235, 235, 235);
            var bgControl = _isDarkTheme ? Color.FromArgb(30, 30, 30) : Color.White;
            var fgText = _isDarkTheme ? Color.FromArgb(220, 220, 220) : Color.Black;

            this.BackColor = bgMain;
            leftPanel.BackColor = bgPanel;
            centerPanel.BackColor = bgMain;
            rightPanel.BackColor = bgMain;

            txtSource.BackColor = bgControl;
            txtSource.ForeColor = fgText;
            txtBBCode.BackColor = bgControl;
            txtBBCode.ForeColor = fgText;
        }
    }
}
