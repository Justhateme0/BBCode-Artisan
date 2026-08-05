namespace BBCode_Artisan
{
    public class CustomTitleBar : Panel
    {
        private readonly Label lblTitle;
        private readonly Button btnMinimize;
        private readonly Button btnMaximize;
        private readonly Button btnClose;
        private readonly Button btnTheme;
        private readonly Form parentForm;
        private Point lastPoint;
        private bool isDragging;

        public event EventHandler? ThemeToggleClicked;

        public CustomTitleBar(Form parent)
        {
            parentForm = parent;
            Height = 40;
            Dock = DockStyle.Top;
            BackColor = Color.FromArgb(30, 30, 30);

            lblTitle = new Label
            {
                Text = "BBCode Artisan",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 40),
                Location = new Point(15, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnTheme = CreateTitleButton("◐", 140);
            btnTheme.Click += (s, e) => ThemeToggleClicked?.Invoke(s, e);

            btnMinimize = CreateTitleButton("─", 90);
            btnMinimize.Click += (s, e) => parentForm.WindowState = FormWindowState.Minimized;

            btnMaximize = CreateTitleButton("□", 50);
            btnMaximize.Click += BtnMaximize_Click;

            btnClose = CreateTitleButton("✕", 10);
            btnClose.Click += (s, e) => parentForm.Close();
            btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.FromArgb(232, 17, 35);
            btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.Transparent;

            Controls.AddRange(new Control[] { lblTitle, btnTheme, btnMinimize, btnMaximize, btnClose });

            PositionButtons();

            MouseDown += TitleBar_MouseDown;
            MouseMove += TitleBar_MouseMove;
            MouseUp += TitleBar_MouseUp;
            lblTitle.MouseDown += TitleBar_MouseDown;
            lblTitle.MouseMove += TitleBar_MouseMove;
            lblTitle.MouseUp += TitleBar_MouseUp;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionButtons();
        }

        // The window buttons are anchored to the right, but they were originally
        // positioned using the parent form's width while this panel was still at its
        // default size, which pushed them off-screen. Lay them out from the panel's
        // actual width instead (right-aligned: theme, minimise, maximise, close).
        private void PositionButtons()
        {
            // OnResize can fire during construction, before the buttons are created.
            if (btnClose == null || btnMaximize == null || btnMinimize == null || btnTheme == null)
                return;

            int w = ClientSize.Width;
            btnClose.Location = new Point(w - 40 - 10, 0);
            btnMaximize.Location = new Point(w - 40 - 50, 0);
            btnMinimize.Location = new Point(w - 40 - 90, 0);
            btnTheme.Location = new Point(w - 40 - 140, 0);
        }

        private Button CreateTitleButton(string text, int rightOffset)
        {
            var btn = new Button
            {
                Text = text,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(40, 40),
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btn.Location = new Point(parentForm.Width - 40 - rightOffset, 0);
            return btn;
        }

        private void BtnMaximize_Click(object? sender, EventArgs e)
        {
            if (parentForm.WindowState == FormWindowState.Maximized)
            {
                parentForm.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "□";
            }
            else
            {
                parentForm.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "❐";
            }
        }

        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastPoint = e.Location;
            }
        }

        private void TitleBar_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var currentScreenPos = PointToScreen(e.Location);
                parentForm.Location = new Point(
                    currentScreenPos.X - lastPoint.X,
                    currentScreenPos.Y - lastPoint.Y
                );
            }
        }

        private void TitleBar_MouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public void UpdateTheme(bool isDarkTheme)
        {
            BackColor = isDarkTheme ? Color.FromArgb(30, 30, 30) : Color.FromArgb(240, 240, 240);
            lblTitle.ForeColor = isDarkTheme ? Color.White : Color.Black;

            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button btn && btn != btnClose)
                {
                    btn.ForeColor = isDarkTheme ? Color.White : Color.Black;
                    btn.FlatAppearance.MouseOverBackColor = isDarkTheme ? Color.FromArgb(60, 60, 60) : Color.FromArgb(200, 200, 200);
                    btn.FlatAppearance.MouseDownBackColor = isDarkTheme ? Color.FromArgb(80, 80, 80) : Color.FromArgb(180, 180, 180);
                }
            }
        }
    }
}
