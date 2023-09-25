using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlyleafLib.MediaPlayer;
using FlyleafLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FlyleafDemo
{
    public class MainViewModel:ObservableObject
    {
        private Player player;

        public Player Player
        {
            get => player;
            set => SetProperty(ref player, value);
        }

        private Config config;

        public Config Config
        {
            get => config;
            set => SetProperty(ref config, value);
        }

        private string uriString;

        public string UriString
        {
            get => uriString;
            set => SetProperty(ref uriString, value);
        }

        public IRelayCommand<string> PlayCommand { get; set; }
        public MainViewModel()
        {
            Config = new Config();
            Config.Video.BackgroundColor = Colors.Transparent;
            // 设置播放延迟为100ms，可能我理解有误，具体可以在项目issues里查看
            // Config.Player.MaxLatency = 100 * 10000;

            Player = new Player(Config);
            PlayCommand = new RelayCommand<string>(PlayAction);
            UriString = uri1;
        }

        private string currentUri = string.Empty;
        private string uri1 = "rtsp://192.168.20.3:554/cam/realmonitor?channel=1&subtype=0&unicast=true&proto=Onvif";
        private string uri2 = "rtsp://192.168.20.4:554/cam/realmonitor?channel=1&subtype=0&unicast=true&proto=Onvif";
        private void PlayAction(string uri)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                if (currentUri == uri1)
                {
                    //Player.Commands.Stop.Execute(null);
                    currentUri = uri2;
                    Player.Commands.Open.Execute(uri2);
                }
                else
                {
                    //Player.Commands.Stop.Execute(null);
                    currentUri = uri1;
                    Player.Commands.Open.Execute(uri1);
                }
            }
        }
    }
}
