   M S B A M L   `   `   `  ���� H  DDiplomProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DiplomProjectDiplomProject       DiplomProject.WindowWorker   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:DiplomProject     -     � Title$%  Тестирование ЦДС��$	��450��$	��800��$��Comic Sans MS)� � 
ResizeMode$ NoResize=����	#FFB8D9F0 � Icon$ icon.ico��.��� .+��� -    ��Name$ buttonLK��$!��Личный кабинет��$
��Left=�$��
663,10,0,0q�$	��Top=�$	��113��$��39�� K� ZIndex$ 1��
�������
���   ��� -    ��Name$ tabControlWorker��$
��Left=�$	��377��$��	10,33,0,0q�$	��Top=�$	��772��
�������
���   �.@��� $��Все тесты��
�������
���   �.��� 
�������.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridTests��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    CanUserAddRows .  	   CanUserDeleteRows	 .  
   
IsReadOnly
 . $
��Left=�$	��271��$��	89,29,0,0q�$	��Top=�$	��635��
�������
���   ��� $��Тесты:��$
��Left=�$��	10,12,0,0q�$	��Top=�$
��Bold"��� -    ��Name$ tabItemTestE��$��Решение теста��{�.  
�������
���   �.��� 
�������.+�  -   $ dataGridVariantsForE�� .  	 .  
 .    AutoGenerateColumns .  $
��Left=�$	��161��$��139,117,0,0q�$	��Top=�$	��475��
���   ����	#FFF1FFFE$��14(�   CanUserSortColumns .     CanUserReorderColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$2 ,Выберите вариант ответа��   Width$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+�� -    ��Name$ labelQuestion��$��quest��$
��Left=�$��	98,60,0,0q�$	��Top=�$��20(��� -   $ buttonAnswer��$%��Следующий вопрос��$
��Left=�$��297,283,0,0q�$	��Top=�$	��165��$��41��
��������� -	   $ 	labelTest��$
��test��$
��Left=�$��	10,10,0,0q�$	��Top=�$
��Bold"�$��Italic$�$��18(��� -
   $ tabItemRemember��$�� Уведомления��
�������
���   �.��� $��	0,1,-1,-4q� � 	Resources  �� Color��%C   (��       * SystemColors.HighlightBrushKey+)�� $ 	#FFF5D0BE��.+�  -   $ dataGridRemembers��$ SizeToCells  .  	 .  
 . $��Right=�$��
0,61,84,45q�$	��613�����	#FFDCFFE3
���   �   HeadersVisibility$
 None=�$��20(�$��Times New Roman)��� $:��4Выберите нужное уведомление��$
��Left=�$��	10,10,0,0q�$	��Top=�$��16(��� -   $ 	labelName��$'��!Добро пожаловать, ��$
��Left=�$��10,6,0,0q�$	��Top=�$
��Bold"�