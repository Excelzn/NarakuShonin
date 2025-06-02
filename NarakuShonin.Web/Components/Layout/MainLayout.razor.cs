namespace NarakuShonin.Web.Components.Layout;

public partial class MainLayout
{
  private bool _isOpen = true;
  
  private void ToggleDrawer() => _isOpen = !_isOpen;
}