# WpfLightToolkit
A light UI toolkit for WPF apps. 

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## Getting Started

To use this repository in one of your own projects, there's a couple things you need to get started.

First of all, install the nuget package
```
Install-Package WpfLightToolkit
```

The next step is to go into `App.xaml` and add the following code under the `<Application.Resources>` tag.
```
<ResourceDictionary>
	<ResourceDictionary.MergedDictionaries>
		<ResourceDictionary Source="/WpfLightToolkit;component/Assets/Default.xaml" />
	</ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

The final step is to change your MainWindow base class from `Window` to `LightWindow`. Remember to change this in both the XAML and the code-behind CS file.

Once you're done with that, you should be ready to start using WpfLightToolkit!

## License

MIT © [Mohamed CHOUCHANE](https://mohachouch.github.io)

