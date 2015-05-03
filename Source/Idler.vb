Imports System.IO
Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Public Class Idler
    Dim myProcesses() As Process
    Dim myProcess As Process
    Dim regKey As RegistryKey 'Clé registre
    Dim regValue As String 'Valeur clé
    Dim pathCS As String 'Chemin final
    Dim pathAppID As String 'Chemin AppID.txt

    Private Const SW_HIDE As Integer = 0 'constante pour cacher une fenêtre
    Private Const SW_RESTORE As Integer = 9 'constante pour restaurer une fenêtre
    Private hwnd As Integer 'Window handle = pointeur de fenêtre


    'TRADUCTION DEBUT
    Dim fr_LANCER As String = "LANCER"
    Dim fr_STOPPER As String = "STOPPER"
    Dim fr_ABOUT As String = "A PROPOS"
    Dim fr_HL2_ON As String = "hl2.exe présent"
    Dim fr_HL2_OFF As String = "hl2.exe absent"
    Dim fr_PATH As String = "Steam Local :"
    Dim fr_CSGO_REQUIERED As String = "*Counter-Strike Source Nécessaire : ??"
    Dim fr_CSGO_ON As String = "*Counter-Strike Source Nécessaire : OK!"
    Dim fr_CSGO_OFF As String = "*Counter-Strike Source Nécessaire : ERREUR!"
    Dim fr_LANGUE As String = "Langue :"
    Dim fr_SAVE As String = "SAUVEGARDE (redémarrage nécessaire)"
    Dim fr_ABOUT_TXT As String = "Créer par NastyZ98. Supporte le framework .NET 3.5 < 4.x" & vbNewLine & "Supporte XP, Vista, Seven et 8.1"
    Dim CSGO_FOUND As String = "*Counter-Strike Source Requiered : FOUND!"
    Dim CSGO_ERROR As String = "*Counter-Strike Source Requiered : ERROR!"
    Dim HL2_OK As String = "hl2.exe is running"
    Dim HL2_OFF As String = "hl2.exe is not running"
    Dim fr_MSGBOX_SAVE As String = "Steam Idler va s'arrêter, réouvrez le manuellement"
    'TRADUCTION FIN

    <DllImport("User32")> _
    Private Shared Function ShowWindow(ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
    End Function 'fonction de la librairie user32.dll commune à tous les Windows


    Private Sub Idler_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        regKey = Registry.CurrentUser.OpenSubKey("Software\Valve\Steam") 'Obtien le path
        regValue = regKey.GetValue("SteamPath") 'Valeur de la clé
        pathCS = regValue & "/steamapps/common/Counter-Strike Source/hl2.exe" 'Path hl2.exe
        pathAppID = regValue & "/steamapps/common/Counter-Strike Source/steam_appid.txt" 'Path de AppID.txt

        path_TXT_BOX.Text = pathCS '
        Timer2.Enabled = True

        If About.BOX_LANGUES.SelectedItem = "Français" Then

            BTN_START.Text = fr_LANCER
            BTN_STOP.Text = fr_STOPPER
            BTN_ABOUT.Text = fr_ABOUT
            LBL_CSGO.Text = fr_CSGO_REQUIERED
            LBL_HL2.Text = fr_HL2_OFF
            LBL_PATH.Text = fr_PATH
            HL2_OFF = fr_HL2_OFF
            HL2_OK = fr_HL2_ON
            CSGO_FOUND = fr_CSGO_ON
            CSGO_ERROR = fr_CSGO_OFF
            About.LBL_LANGUE.Text = fr_LANGUE
            About.BTN_SAVE.Text = fr_SAVE
            About.Label1.Text = fr_ABOUT_TXT
            About.SteamEngineTheme1.Text = "A Propos & Paramètres"
            About.MSGBOX_SAVE = fr_MSGBOX_SAVE
        End If
        If System.IO.Directory.Exists(pathCS) = False Then
            System.IO.Directory.CreateDirectory(regValue & "/steamapps/common/Counter-Strike Source/")
            System.IO.Directory.CreateDirectory(regValue & "/steamapps/common/Counter-Strike Source/bin/")
            File.WriteAllBytes(regValue & "/steamapps/common/Counter-Strike Source/hl2.exe", My.Resources.hl2)
            File.WriteAllBytes(regValue & "/steamapps/common/Counter-Strike Source/bin/launcher.dll", My.Resources.launcher)
            File.WriteAllBytes(regValue & "/steamapps/common/Counter-Strike Source/bin/Steam.dll", My.Resources.Steam)
            File.WriteAllBytes(regValue & "/steamapps/common/Counter-Strike Source/bin/steam_api.dll", My.Resources.steam_api)
            File.WriteAllBytes(regValue & "/steamapps/common/Counter-Strike Source/bin/tier0.dll", My.Resources.tier0)
            File.WriteAllBytes(regValue & "/steamapps/common/Counter-Strike Source/bin/vstdlib.dll", My.Resources.vstdlib)
            File.WriteAllText(regValue & "/steamapps/common/Counter-Strike Source/steam_appid.txt", My.Resources.steam_appid)
        End If
        
    End Sub

    Private Sub BTN_1_Click(sender As Object, e As EventArgs) Handles BTN_START.Click
        'Lors du lancement du programme

        Timer1.Start() 'On démarre le timer

        'Ecrire dans le fichier
        Dim SW As New StreamWriter(pathAppID)
        SW.Write(appID_TXT_BOX.Text)
        SW.Close()

        'Gère l'exception
        Try 'Essaye si fonctionne
            Shell(pathCS, 0)

        Catch Ex As Exception 'Si l'exception 
            MsgBox("Erreur impossible de lancer le processus hl2.exe", vbOKOnly + MsgBoxStyle.Exclamation)
        End Try 'Fin exception

        BTN_START.Enabled = False
        BTN_STOP.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'par défaut, toutes les 100 milisecondes on arrive ici (Timer1.interval = x pour changer cette valeur)
        For Each p As Process In Process.GetProcessesByName("hl2") 'à remplacer par un vrai nom
            'pour tous les processus qui contiennent le nom recherché
            If p.ToString.Contains("hl2") Then
                'on cache le processus recherché
                hwnd = CType(p.MainWindowHandle, Integer) 'on obtient le pointeur de sa fenêtre
                ShowWindow(hwnd, SW_HIDE) 'on la cache (SW_RESTORE pour ré-afficher, au cas où)
            End If
        Next


    End Sub

    Public Sub BTN_STOP_Click_1(sender As Object, e As EventArgs) Handles BTN_STOP.Click
        Timer1.Stop()

        'Retourne toutes les instances du processus "hl2".
        myProcesses = Process.GetProcessesByName("hl2")

        Try
            For Each myProcess In myProcesses 'Pour chaque "var" dans "var"

                myProcess.Kill()

            Next
            If myProcess.HasExited = True Then

                BTN_STOP.Enabled = False
                BTN_START.Enabled = True

            End If

        Catch ex As Exception

            MsgBox("Erreur 0x800 : Impossible d'arrêter le processus hl2.exe", vbOKOnly + MsgBoxStyle.Critical)

        End Try

    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub ReduceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReduceToolStripMenuItem.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick


        myProcesses = Process.GetProcessesByName("hl2")
        If myProcesses.Count > 0 Then
            LBL_HL2.Text = HL2_OK
            LBL_HL2.ForeColor = Color.LimeGreen
        Else
            LBL_HL2.Text = HL2_OFF
            LBL_HL2.ForeColor = Color.Yellow
        End If

        If System.IO.File.Exists(pathCS) Then
            LBL_CSGO.Text = CSGO_FOUND
            LBL_CSGO.ForeColor = Color.LimeGreen
        Else
            LBL_CSGO.Text = CSGO_ERROR
            LBL_CSGO.ForeColor = Color.Red
        End If


    End Sub

    Private Sub SteamButton1_Click(sender As Object, e As EventArgs) Handles BTN_ABOUT.Click
        About.Show()
        Me.Visible = False
    End Sub

End Class
