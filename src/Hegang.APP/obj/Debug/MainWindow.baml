   M S B A M L   `   `   `  ����E  AHegang.APP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Hegang.APP
Hegang.APP  5	   	       Hegang.APP.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Hegang.APP     5	   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��650��6)    � WindowStyle$
 None=�65    � 	Resources 5       �� 
TargetType �� 	Resources �� Color��%�   'Q�       �� 5   
   " Q�6    5      ���� 5      $ White��6T   (��       6"   * SystemColors.HighlightBrushKey+)6T   5      5      5      .��5      ]� .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��Right=�6C   $��0,0,10,0q�6_   .+�5      �� -   5        $��minimize_btn��5      
�������$��30��6S   ��6<   Z� .��5      �� $��./images/minimize.png��6   $��20��67   6D   5      �� 5      -   5        $��	close_btn��5      
�������$��30��6P   ��69   Z� .��5      �� $��./images/close.png��6   $��20��64   6A   5      5      � 5   
   $	��Top=�6   $��
10,15,10,0q�6$    � ColumnDefinitions 5      �� 5      $��3* �6#   60   �� 5       $��7* �6#   60   �� 5!      $��2* �6#   60   �� 5"      $��* �6#   6/   .+�5$      �� -   5        $��btn_connect��5$      $��0��6(   
�������$��加载并连接OPC��6K   $��0,0,5,0q�6^   6o   �� 5%      -   5        $��cmb_server_list��5%      $��1��6.   $��Center=�6>    �� SelectedIndex$ 0��6Y   �� 5&      -   5        $��btn_read��5&   G   $��2��6   
�������$��读取数据��68   $��5,0,5,0q�6n   �� 5'      $��3��6   
�������$��停止��68   $��0,0,0,0q�6E   5(      � 5)   
   $��10,10,10,10q�6    5*      �� 5+      $��* �6#   6/   �� 5,      $
��auto �6#   62   �� 5-      $��3* �6#   $	��400��6.   6?   .+�5/      R� -   5        $��chk_treeview��5/   '   $��0��6    	 �� IsTextSearchCaseSensitive	 . ��50      �� 51      " Q�6   .�52      �� 
 �� Property
 �@Q�
IsExpanded �� Value 67   �� *
IsExpanded+ �� Mode$ TwoWay=�6c   53      ��55      �� 56       �� ItemsSource 6/   ��  �� Path$ 	ChildList�.R�57      �� $��checkbox��6#    d� 	IsChecked 63   �� $ 	IsChecked�$ TwoWay=�.��58      �� ��6/   �� $ NodeName�59      5:      5<      �� 5=      $��1��6   $��3��6+   
�������$��Stretch=�6H   $��Center=�6d   6�   �� 5>      $��2��6   5@      5A      5B      5C      