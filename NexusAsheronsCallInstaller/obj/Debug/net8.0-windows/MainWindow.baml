   M S B A M L   `   `   `  ����U  QNexusAsheronsCallInstaller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:NexusAsheronsCallInstallerNexusAsheronsCallInstaller  5   	   +    %NexusAsheronsCallInstaller.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:NexusAsheronsCallInstaller     5   	     � Title$$  Nexus Asheron's Call Installer��5   	   $	��500��60   $	��350��6=   ���#2D2D30 � 	Resources 5
      6   0NexusAsheronsCallInstaller.StatusToIconConverter   StatusToIconConverter �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value��%�  &	        '��        5   
   6&   �� 5   
   " ��6   .�5      ��  � �� �	#FF9300FE6+   �� 5       � ��
 �����6+   �� 5       � ��$ 24(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 80,40q�6(   �� 5       � ��$ 2q�60   �� 5       � ��
 �   �6,   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��6K   #��z��6w   $ 5��6�   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver$
	 True��6=   . �5   "   ��  � �� �	#FF7A00CC6?   5      5       5"      5#      5$      .��5%      � ��5&   
   �� 5'      
 �� ImageSource$
 background.png��6    j� Stretch$ UniformToFill=�66   .+�5)   
   � $��15q�6    � RowDefinitions 5*      �� 5+      $
��Auto �6    �� 5,      $��* �6    �� 5-      $
��Auto �6    .+�50      �� $��Nexus AC Installer��6   $��0��62   $��20(�6?   $
��Bold"�6M   ���	#FF9400FF$��Center=�6v   $��0,0,0,15q�6�    J� Effect 51      3  -System.Windows.Media.Effects.DropShadowEffect  52         Color$ 	#FF9400FF��6'      ShadowDepth$ 0Q�69      
BlurRadius$ 8Q�6I   54      �� 56      $��1��6   ���	#80000000$ 5��69   ._�57      �� -   5         ��Name$ 	Checklist��57      
������ $��0q�6G   
���������58      �� 59      .R�5:      � $	��0,5q�6#    � ColumnDefinitions 5;   "   �� 5<   &   $
��Auto �67   �� 5=   &   $��* �67   �� 5>   &   $
��Auto �67   .+�5@   "   �� $��0��6v   $��Center=�6�   $��0,0,10,0q�6�   ���	#FF9400FF��6,   �� *Status+ �� 	Converter# [  6�   �� 5A   "   $��1��6B   $��Center=�6R   ��6,   �� *Name+6R   �� 5B   "   $��2��6Y   $��Center=�6i    d� 	IsChecked 6+   �� *
IsSelected+ �� Mode$ TwoWay=�6i   5C      5D      5F      5G      �� 5I      $��2��6   .+�5J      � -   5         �Name$ OverallProgressBar��5J      $��8��6:   $��0,15,0,0q�6E   �� 5K      -   5         ��Name$ StatusTextBlock��5K      $��Ready to begin.��65   
�������$��Center=�6_   $��0,5,0,0q�6|   �� 5L      -   5         ��Name$ InstallButton��5L      $��Install��60   $��Center=�6^   $��0,15,0,0q�6{   5M      5N      5O      5P      