   M S B A M L   `   `   `  ���� U  QNexusAsheronsCallInstaller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:NexusAsheronsCallInstallerNexusAsheronsCallInstaller  +    %NexusAsheronsCallInstaller.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:NexusAsheronsCallInstaller       � Title$$  Nexus Asheron's Call Installer��$	��500��$	��350�����#2D2D30 � 	Resources 6   0NexusAsheronsCallInstaller.StatusToIconConverter   StatusToIconConverter �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value��%�  &	        '��        �� " ��.���  � �� �	#FF9300FE��  � ��
 �������  � ��$ 24(���  � ��$
 Bold"���  � ��$ 80,40q���  � ��$ 2q���  � ��
 �   ���  �@�Cursor$
 Hand����  � �� �� " ��.R��� #��z��#��z��#��z��$ 5��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
	 True��. ���  � �� �	#FF7A00CC.��� ���� 
 �� ImageSource$
 background.png�� j� Stretch$ UniformToFill=�.+�� $��15q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��Nexus AC Installer��$��0��$��20(�$
��Bold"����	#FF9400FF$��Center=�$��0,0,0,15q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ 	#FF9400FF��   ShadowDepth$ 0Q�   
BlurRadius$ 8Q��� $��1�����	#80000000$ 5��._��� -    ��Name$ 	Checklist��
������ $��0q�
����������� .R�� $	��0,5q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Center=�$��0,0,10,0q����	#FF9400FF���� *Status+ �� 	Converter# [  �� $��1��$��Center=����� *Name+�� $��2��$��Center=� d� 	IsChecked �� *
IsSelected+ �� Mode$ TwoWay=��� $��2��.+�� -    �Name$ OverallProgressBar��$��8��$��0,15,0,0q��� -    ��Name$ StatusTextBlock��$��Ready to begin.��
�������$��Center=�$��0,5,0,0q��� -    ��Name$ InstallButton��$��Install��$��Center=�$��0,15,0,0q�