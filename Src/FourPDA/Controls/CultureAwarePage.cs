// Decompiled with JetBrains decompiler
// Type: FourPDA.Controls.CultureAwarePage
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace FourPDA.Controls
{
  public class CultureAwarePage : PhoneApplicationPage
  {
    public CultureAwarePage() => ((FrameworkElement) this).Language = XmlLanguage.GetLanguage("ru");
  }
}
