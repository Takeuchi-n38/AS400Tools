using System;
//using System.Collections.Generic;
using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using System.Reflection;
//using System;
//using Delta.AS400.Koubai01.Oplib.Cmenok;
//using Delta.AS400.Koubai01.Kcslib.Cmenmain;
//using Delta.AS400.Koubai01.Kcslib.Csopchks;
//using Delta.AS400.Koubai01.Kcslib.Gyotbls;
//using Delta.AS400.Koubai01.Kcslib.Kmkpcas;
//using Delta.AS400.Koubai01.Kcslib.Krytbls;
//using Delta.AS400.Koubai01.Kcslib.Ktanims;
//using Delta.AS400.Koubai01.Kcslib.Ktanhms;
//using Delta.AS400.Koubai01.Kcslib.Icsm000;
//using Delta.AS400.Koubai01.Kcslib.Utanhms;
//using Delta.AS400.Koubai01.Kcslib.Utanims;
//using Delta.AS400.Koubai01.Kcslib.Icsu000;
//using Delta.AS400.Koubai01.Kcslib.Urytbls;
//using Delta.AS400.Koubai01.Kcslib.Icsc000;
//using Delta.AS400.Koubai01.Kcslib.Icsc010;
//using Delta.AS400.Koubai01.Kcslib.Icsc005;
//using Delta.AS400.Koubai01.Kcslib.Icsc015;
//using Delta.AS400.Koubai01.Kcslib.Icsu905;
//using Delta.AS400.Koubai01.Kcslib.Icsu900;
//using Delta.AS400.Koubai01.Kcslib.Icsu100;
//using Delta.AS400.Koubai01.Kcslib.Icsu020;
//using Delta.AS400.Koubai01.Kcslib.Icsu010;
//using Delta.AS400.Koubai01.Kcslib.Icsu110;
//using Delta.AS400.Koubai01.Kcslib.Icsk000;
//using Delta.AS400.Koubai01.Kcslib.Icsk010;
//using Delta.AS400.Koubai01.Kcslib.Icsk020;
//using Delta.AS400.Koubai01.Kcslib.Icsk100;
//using Delta.AS400.Koubai01.Kcslib.Icsk110;
//using Delta.AS400.Koubai01.Kcslib.Icsk900;
//using Delta.AS400.Koubai01.Kcslib.Icsk905;
using System.Net.Http;
//using Delta.AS400.Emulator.Workstation.Sndusrmsg;
//using Delta.AS400.Emulator.Job;
//using Delta.AS400.Koubai01.Kcslib.Jvaf40mu;
//using Delta.AS400.Koubai01.Kcslib.Jvaf30mk;
//using Delta.AS400.Koubai01.Kcslib.Jvaf40mk;
//using Delta.AS400.Koubai01.Kcslib.Jvaf30mu;
//using Delta.AS400.Koubai01.Kcslib.Cbkupmlto;
//using Delta.AS400.Koubai01.Kcslib.Jvaf;
using Unity;
//using Delta.AS400.Honsha01.Prodlib.Immstrs;
using NLog;
//using Delta.AS400.Koubai01.Salelib.Cmenmenus;
//using System.Configuration;

namespace Delta.Tools.AS400.Modernizer.GuiApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// アプリケーション開始時のイベントハンドラ
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.DispatcherUnhandledException += Application_DispatcherUnhandledException;// UIスレッドで実行されているコードで処理されなかったら発生する（.NET 3.0より）CleanUped
            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;// バックグラウンドタスク内で処理されなかったら発生する（.NET 4.0より）
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;// 例外が処理されなかったら発生する（.NET 1.0より）CleanUped

            //AppDomain.CurrentDomain.FirstChanceException += (o, eh) =>
            //{
            //    Exception _ex = eh.Exception;
            //    MessageBox.Show(_ex.ToString());
            //};

            //TaskScheduler.UnobservedTaskException += (o, eh) =>
            //{
            //    Exception _ex = eh.Exception;
            //    MessageBox.Show(_ex.ToString());

            //    // 処理済みとしてアプリを終了させないために以下メソッドを呼び出します。
            //    eh.SetObserved();
            //};

            //if (ProcessHelper.IsAlreadyLaunched)
            //{
            //    MessageDlgPresenter.ShowError(AppRS.This_software_is_already_launched);
            //    this.Shutdown();
            //    return;
            //}

            //try
            //{
            //    HuTachoSystemRepository.Find().ChangeCulture();
            //}
            //catch (HuTachoApplicationException ex)
            //{
            //    MessageDlgPresenter.ShowError(ex.Message);
            //    this.Shutdown();
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    MessageDlgPresenter.ShowError(String.Format(UIRS.An_error_occurred_Quit_program_imediately, HuTachoService.loggingError(ex).FullName));
            //    this.Shutdown();
            //    return;
            //}

            //_mainPresenter = new Presenter.MainWindowPresenter();//.Current;
            //_mainPresenter.Initialize();

            //this.MainWindow = _mainPresenter.View;
            //this.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.MainWindow.WindowState = WindowState.Maximized;
            //this.MainWindow.Title = $"{ApplicationRS.Hu_tacho}  ver.{HuTachoExe.Version}";

            //this.MainWindow.Show();
        }


        /// <summary>ViewとViewModelの名前付け規則を設定します。</summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(vt =>
            {
                var viewName = vt.FullName;
                var asmName = vt.GetTypeInfo().Assembly.FullName;
                //var vmName = $"{viewName}ViewModel, {asmName}";
                var vmName = $"{viewName}Model, {asmName}";

                return Type.GetType(vmName);
            });
        }

        ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //logger.Info("info");

            containerRegistry.RegisterInstance<ILogger>(logger);

            //var cswebUri = ConfigurationManager.AppSettings["Uri:cs-web"];

            //var aHttpClient = new HttpClient { BaseAddress = new Uri(cswebUri), Timeout = TimeSpan.FromMinutes(30) };

            //containerRegistry.RegisterInstance<HttpClient>(aHttpClient);

            //containerRegistry.RegisterDialog<SndUsrInfMsgView, SndUsrInfMsgViewModel>();
            //containerRegistry.RegisterDialog<SndUsrInqMsgView, SndUsrInqMsgViewModel>();

            //containerRegistry.RegisterDialog<DmenmainView, DmenmainViewModel>();

            //containerRegistry.RegisterDialog<DmenokView, DmenokViewModel>();

            //containerRegistry.RegisterDialog<Bcsc000View, Bcsc000ViewModel>();
            //containerRegistry.RegisterDialog<Bcsc010View, Bcsc010ViewModel>();
            //containerRegistry.RegisterDialog<Bcsc005View, Bcsc005ViewModel>();
            //containerRegistry.RegisterDialog<Bcsc015View, Bcsc015ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk000View, Bcsk000ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk010View, Bcsk010ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk020View, Bcsk020ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk100View, Bcsk100ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk110View, Bcsk110ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk900View, Bcsk900ViewModel>();
            //containerRegistry.RegisterDialog<Bcsk905View, Bcsk905ViewModel>();

            //containerRegistry.RegisterDialog<Bcsu000View, Bcsu000ViewModel>();
            //containerRegistry.RegisterDialog<Bcsu010View, Bcsu010ViewModel>();
            //containerRegistry.RegisterDialog<Bcsu020View, Bcsu020ViewModel>();
            //containerRegistry.RegisterDialog<Bcsu100View, Bcsu100ViewModel>();
            //containerRegistry.RegisterDialog<Bcsu110View, Bcsu110ViewModel>();
            //containerRegistry.RegisterDialog<Bcsu900View, Bcsu900ViewModel>();
            //containerRegistry.RegisterDialog<Bcsu905View, Bcsu905ViewModel>();

            //var CsopchkRepository = new CsopchkRepository();
            //containerRegistry.RegisterInstance<ICsopchkRepository>(CsopchkRepository);
            //containerRegistry.RegisterInstance<IUrytblRepository>(new UrytblRepository());
            //containerRegistry.RegisterInstance<IUtanhmRepository>(new UtanhmRepository());
            //containerRegistry.RegisterInstance<IUtanimRepository>(new UtanimRepository());
            //containerRegistry.RegisterInstance<IGyotblRepository>(new GyotblRepository());
            //IKmkpcaRepository KmkpcaRepository = new KmkpcaRepository();
            //containerRegistry.RegisterInstance<IKmkpcaRepository>(KmkpcaRepository);
            //containerRegistry.RegisterInstance<IKrytblRepository>(new KrytblRepository());
            //containerRegistry.RegisterInstance<IKtanimRepository>(new KtanimRepository());
            //containerRegistry.RegisterInstance<IKtanhmRepository>(new KtanhmRepository());

            //var webAPIFx = ConfigurationManager.AppSettings["Uri:Honsha01.Prodlib.WebAPIFx"];

            //containerRegistry.RegisterInstance<IImmstrRepository>(new WebAPIImmstrRepository(new HttpClient { BaseAddress = new Uri(webAPIFx) }));

            //IUnityContainer container = containerRegistry.GetContainer();
            //var aDialogService = container.Resolve<IDialogService>();
            //containerRegistry.RegisterInstance<Icsc000Model>(new Icsc000Model(aDialogService,KmkpcaRepository));

            //containerRegistry.RegisterInstance<JobAttribute>(new JobAttribute());

            //var aJvafService = new JvafService(aHttpClient);

            //IJvaf30mkService aJvaf30mkService;
            //IJvaf40mkService aJvaf40mkService;
            //IJvaf30muService aJvaf30muService;
            //IJvaf40muService aJvaf40muService;

            //var isTest = false;
            //if (isTest)
            //{
            //    aJvaf30mkService = new Jvaf30mkService(CsopchkRepository);
            //    aJvaf40mkService = new Jvaf40mkService(CsopchkRepository);
            //    aJvaf30muService = new Jvaf30muService(CsopchkRepository);
            //    aJvaf40muService = new Jvaf40muService(CsopchkRepository);
            //}
            //else
            //{
            //    aJvaf30mkService = aJvafService;
            //    aJvaf40mkService = aJvafService;
            //    aJvaf30muService = aJvafService;
            //    aJvaf40muService = aJvafService;
            //}
            //containerRegistry.RegisterInstance<IJvaf30mkService>(aJvaf30mkService);
            //containerRegistry.RegisterInstance<IJvaf40mkService>(aJvaf40mkService);
            //containerRegistry.RegisterInstance<IJvaf30muService>(aJvaf30muService);
            //containerRegistry.RegisterInstance<IJvaf40muService>(aJvaf40muService);

            //containerRegistry.RegisterInstance<ICbkupmltoService>(new CbkupmltoService(CsopchkRepository));
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<StartUpWindowView>();
        }

        /// <summary>
        /// WPF UIスレッドでの未処理例外スロー時のイベントハンドラ
        /// </summary>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            this.ReportUnhandledException(e.Exception);

            // ハンドルされない例外を処理済みにするためにtrueを指定
            e.Handled = true;

            this.Shutdown();
        }

        /// <summary>
        /// UIスレッド以外の未処理例外スロー時のイベントハンドラ
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.ReportUnhandledException(e.ExceptionObject as Exception);

            // このまま終わると動作を停止しましたというウインドウが出てしまうので
            // 挙動を抑えるために自分で先んじで終了する
            Environment.Exit(-1);
        }


        /// <summary>
        /// 未処理例外をイベントログに出力します。
        /// </summary>
        private void ReportUnhandledException(Exception ex)
        {
            logger.Error(ex.ToString());
            MessageBox.Show("予期しないエラーが発生しました。システム管理者に問い合わせてください。");
            //try
            //{
            //    MessageDlgPresenter.ShowError(String.Format(UIRS.An_error_occurred_Quit_program_imediately, HuTachoService.loggingError(ex).FullName));
            //}
            //catch (Exception e)
            //{
            //    MessageDlgPresenter.ShowError(String.Format(UIRS.An_error_occurred_Quit_program_imediately, e.ToString()));
            //}

            //LogManager.Shutdown();

        }


        /// <summary>
        /// アプリケーション終了時のイベントハンドラ
        /// </summary>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //_mainPresenter?.CleanUp();

            this.DispatcherUnhandledException -= Application_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;

        }
    }

}
