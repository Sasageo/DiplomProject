   M S B A M L   `   `   `  ����H  DDiplomProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DiplomProjectDiplomProject  5   	        DiplomProject.WindowWorker   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DiplomProject     5   	   -   5          � Title$%  Тестирование ЦДС��5   	   $	��450��6"   $	��800��6/   $��Comic Sans MS)�6R    � 
ResizeMode$ NoResize=�6m   ���	#FFB8D9F0 � Icon$ icon.ico��6�   .��5	      � .+�5
   
   �� -   5         ��Name$ buttonLK��5
      $!��Личный кабинет��6#   $
��Left=�6<   $��
663,10,0,0q�6W   $	��Top=�6k   $	��113��6�   $��39��6�    K� ZIndex$ 1��6�   
�������
���   �6�   �� 5   
   -   5         ��Name$ tabControlWorker��5      $
��Left=�6/   $	��377��6J   $��	10,33,0,0q�6W   $	��Top=�6j   $	��772��6�   
�������
���   �.@�5      �� $��Все тесты��6   
�������
���   �.��5      � 
�������.+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridTests��5         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 66      CanUserAddRows .  	   CanUserDeleteRows	 .  
   
IsReadOnly
 . $
��Left=�6�   $	��271��6�   $��	89,29,0,0q�6�   $	��Top=�6�   $	��635��6  
�������
���   �6,  �� 5      $��Тесты:��6   $
��Left=�6-   $��	10,12,0,0q�6H   $	��Top=�6[   $
��Bold"�6s   5      5      �� 5      -   5         ��Name$ tabItemTestE��5      $��Решение теста��6,   {�.  
�������
���   �.��5      � 
�������.+�5        -   5        $ dataGridVariantsForE��5       .  	 .  
 .    AutoGenerateColumns .  $
��Left=�6�   $	��161��6�   $��139,117,0,0q�6�   $	��Top=�6�   $	��475��6�   
���   ����	#FFF1FFFE$��14(�6)     CanUserSortColumns .     CanUserReorderColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$2 ,Выберите вариант ответа��61      Width$ * 6k   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6R   �� *Name+6k   5      �� 5      -   5        $ buttonAnswer��5      $%��Следующий вопрос��63   $
��Left=�6N   $��297,283,0,0q�6i   $	��Top=�6~   $	��165��6�   $��41��6�   
�������6�   �� 5      -   5         ��Name$ 	labelTest��5      $
��test��6/   $
��Left=�6>   $��	10,10,0,0q�6Y   $	��Top=�6l   $
��Bold"�6�   $��Italic$�6�   $��18(�6�   �� 5      -	   5         ��Name$ textBlockQuestion��5       $
��Left=�6;   $��	90,65,0,0q�6V   $
��Wrap=�6i   $	��Top=�6}   
���   �$��quest��6�   $��16(�6�   $��47��6�   $	��584��6�   5      5      �� 5      -
   5        $ tabItemRemember��5      $�� Уведомления��6/   
�������
���   �.��5      � $��	0,1,-1,-4q�6    � 	Resources 5        �� Color��%V   (��       5!   *   * SystemColors.HighlightBrushKey+)�� 6   $ 	#FFF5D0BE��6\   5"      .+�5#        -   5        $ dataGridRemembers��5#      $ SizeToCells 6:    .  	 .  
 . $��Right=�6�   $��
0,61,84,45q�6�   $	��613��6�   ���	#FFDCFFE3
���   �   HeadersVisibility$
 None=�6,  $��20(�6E  $��Times New Roman)�6S  6p  �� 5$      $:��4Выберите нужное уведомление��6   $
��Left=�6B   $��	10,10,0,0q�6]   $	��Top=�6p   $��16(�6�   5%      5&      5'      �� 5(   
   -   5        $ 	labelName��5(      $'��!Добро пожаловать, ��6#   $
��Left=�6@   $��10,6,0,0q�6[   $	��Top=�6m   $
��Bold"�6�   5*      5+      