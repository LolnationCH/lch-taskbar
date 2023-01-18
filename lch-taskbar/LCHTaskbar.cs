using System.Windows.Forms;

public partial class LCHTaskbar : Form
{
  public LCHTaskbar()
  {
    // InitializeComponent();

    // Make the form topmost
    this.TopMost = true;
    this.TopLevel = true;

    // Make the form transparent
    this.ControlBox = false;
    this.FormBorderStyle = FormBorderStyle.None;

    // Set the position to the top left corner
    this.StartPosition = FormStartPosition.Manual;
    this.Location = new System.Drawing.Point(0, 0);

    Button button = new Button();
    button.Text = "Close";
    //button.Click += (sender, e) => this.Close();
    this.Controls.Add(button);
    this.Focus();
  }
  protected override bool ShowWithoutActivation
  {
    get { return true; }
  }

  protected override void OnPaintBackground(PaintEventArgs e) { /* Ignore */ }
}

public static class LCHTaskbarHandler
{
  public static LCHTaskbar? LCHTaskbar = null;

  private static void Show()
  {
    if (LCHTaskbar == null)
      LCHTaskbar = new LCHTaskbar();

    LCHTaskbar.Show();
  }

  private static void Hide()
  {
    if (LCHTaskbar != null)
      LCHTaskbar.Hide();
  }

  public static void Toggle()
  {
    if (LCHTaskbar != null &&
        LCHTaskbar.Visible)
      Hide();
    else
      Show();
  }
}