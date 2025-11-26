using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.Components;

public partial class InvoiceItemListComponent : ContentView
{
    public static readonly BindableProperty InvoiceItemProperty = BindableProperty.Create(
         propertyName: nameof(InvoiceItem),
         returnType: typeof(InvoiceItemModel),
         declaringType: typeof(InvoiceItemListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public InvoiceItemModel InvoiceItem
    {
        get => (InvoiceItemModel)GetValue(InvoiceItemProperty);
        set => SetValue(InvoiceItemProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(InvoiceItemListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public static readonly BindableProperty EditCommandProperty = BindableProperty.Create(
         propertyName: nameof(EditCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(InvoiceItemListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: nameof(CommandParameter),
         returnType: typeof(string),
         declaringType: typeof(InvoiceItemListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );

    public IAsyncRelayCommand EditCommand
    {
        get => (IAsyncRelayCommand)GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public InvoiceItemListComponent()
	{
		InitializeComponent();
	}
}