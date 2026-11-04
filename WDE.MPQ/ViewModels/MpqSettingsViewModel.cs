using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using WDE.Common;
using WDE.Common.Managers;
using WDE.Common.Services.MessageBox;
using WDE.Common.Types;
using WDE.Common.Utils;
using WDE.Module.Attributes;

namespace WDE.MPQ.ViewModels
{
    [AutoRegister(Platforms.Desktop)]
    public class MpqSettingsViewModel : BindableBase, IFirstTimeWizardConfigurable
    {
        private string? woWPath;
        private MpqOpenType mpqOpenType;

        public MpqSettingsViewModel(IMpqSettings mpqSettings, 
            IWoWFilesVerifier verifier, 
            IWindowManager windowManager,
            IMessageBoxService messageBoxService)
        {
            woWPath = mpqSettings.Path;
            mpqOpenType = mpqSettings.OpenType;
            
            Save = new DelegateCommand(() =>
            {
                mpqSettings.Path = woWPath;
                mpqSettings.OpenType = mpqOpenType;
                mpqSettings.Save();
                IsModified = false;
                RaisePropertyChanged(nameof(IsModified));
            });

            PickFolder = new AsyncAutoCommand(async () =>
            {
                var folder = await windowManager.ShowFolderPickerDialog(woWPath ?? "");
                if (folder != null)
                {
                    var type = verifier.VerifyFolder(folder);
                    if (type == WoWFilesType.Invalid)
                    {
                        await messageBoxService.ShowDialog(new MessageBoxFactory<bool>()
                            .SetTitle("WoW Client Data")
                            .SetMainInstruction("Invalid WoW folder")
                            .SetContent(
                                "This doesn't look like a correct WoW folder.\n\nSelect main game folder (the folder with wow.exe and the Data folder). For 8.x clients you can select either the World of Warcraft folder or its _retail_ folder.\n\nOther WoW versions are not supported now.")
                            .WithOkButton(true)
                            .Build());
                    }
                    else
                    {
                        WoWPath = verifier.ResolveGameFolder(folder) ?? folder;
                        if (type == WoWFilesType.Retail)
                        {
                            await messageBoxService.ShowDialog(new MessageBoxFactory<bool>()
                                .SetTitle("WoW Client Data")
                                .SetMainInstruction("Retail (8.x+) client files")
                                .SetContent(
                                    "The folder was recognized as a retail (8.x or newer) client.\n\nThe 3D map and model preview do not support these client files yet. Database editing does not need this setting; for spell, area, faction and other names configure the DBC settings with the extracted dbc folder of your server instead.")
                                .WithOkButton(true)
                                .Build());
                        }
                    }
                }
            });
        }
        
        public ICommand PickFolder { get; }

        public string? WoWPath
        {
            get => woWPath;
            set
            {
                SetProperty(ref woWPath, value);
                IsModified = true;
                RaisePropertyChanged(nameof(IsModified));
            }
        }
        
        public List<MpqOpenType> MpqOpenTypes { get; } = Enum.GetValues(typeof(MpqOpenType)).Cast<MpqOpenType>().ToList();
        
        public MpqOpenType MpqOpenType
        {
            get => mpqOpenType;
            set
            {
                SetProperty(ref mpqOpenType, value);
                IsModified = true;
                RaisePropertyChanged(nameof(IsModified));
            }
        }

        public ICommand Save { get; set; }
        public string Name => "Client data files";
        public ImageUri Icon { get; } = new ImageUri("Icons/document_file_big.png");
        public string ShortDescription =>
            "The editor can open 3.3.5, 4.3.4, 5.4.8, 7.3.5 and retail 8.3.7 files for extended features.";
        public bool IsModified { get; set; }
        public bool IsRestartRequired => true;
        public ConfigurableGroup Group => ConfigurableGroup.Basic;
    }
}