   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Client.ViewsClient.Views  5   	        Client.Views.GetValuesView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Client.Views     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ 	GetValues��5	   	   $	��450��6   $	��800��6(   .��5
      � .+�5   
   c� $
��Left=�6   $��39��6-   $	��Top=�69   $	��793��6Q   .@�5      �� -   5         ��Name$ GetValuesButton��5      $��	GetValues��6.   $
��Left=�6B   $��10,0,0,0q�6]   $	��Top=�6o   $��84��6�   $��43��6�   $��MV Boli)�6�   �� 5      -   5        $ GetExtentValuesButton��5      $��GetExtentValues��64   $
��Left=�6r   $��0,0,0,-9.667q�6�   $	��Top=�6�   $	��102��6�   $��43��6�   $��MV Boli)�6�   �� 5      -   5        $ GetRelatedValuesButton��5      $��GetRelatedValues��65   $
��Left=�6u   $��0,0,0,-9.667q�6�   $	��Top=�6�   $	��111��6�   $��43��6�   $��MV Boli)�6�   5      �� 5   
   -   5         ��Name$ GidLabel��5      $��14(�6"   $
��Bold"�60   
���   �$
��GID:��6U   $
��Left=�5      $��	10,30,0,0q�6/   $	��Top=�6B   $	��282��6Z   $��MV Boli)�6f   �� 5   
   -   5         ��Name$ GidComboBoxView��5       �� ItemStringFormat$ 	0x{0:x16}��6,   $��	10,64,0,0q�5   M   $��26��6a   $	��Top=�6m   $
��Left=�6�   $	��372��6�   ��5   I   ��  �� Path$ GidComboBox� �� SelectedItem 5      �� $ SelectedGidFromComboBox�6�   �� 5   
   -   5        $ AttributesLabel��5      $��14(�6)   $
��Bold"�67   
���   �$��	Property:��6\   $
��Left=�6p   $��	10,94,0,0q�6�   $	��Top=�6�   $��31��6�   $	��318��6�   $��MV Boli)�6�   �� 5   
   -   5         ��Name$ propsListBox��5      	 �� SelectionMode$	 Multiple=�5      
�������$��10,125,410,48q�6C   ��5   )   �� $ PropsForSelectedGid�
 �� UpdateSourceTrigger$
 PropertyChanged=���5      �� 5      .R�5      � .+�5      �� $��3q�6$   ��6/   ��  d� 	IsChecked 6C   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" ��$ 
IsSelected� �� Mode$ TwoWay=�5      5      5!      �� 5#   
   -   5        $ GetValuesViewResultButton��5#      $��	GetValues��64   $��	10,0,0,10q�6H   $��24��6[   $��Bottom=�6g   $
��Left=�5$      $	��372��60   
���   �$
��Bold"�6O   $��MV Boli)�6�   �� 5&   
   -	   5        $ ResultLabel��5&      $��Ispis:��6%   
���   �$��Center=�6I   $��414,35,23,0q�5'      $��36��6)   $	��Top=�65   $��16(�6M   $
��Bold"�6[   $��MV Boli)�6m   �� 5(   
   -
   5         ��Name$ ResultTextBox��5(      ��.  �� 
IsReadOnly . $��414,71,10,10q�6c   .��6y   ��5)      �� 5*      .�5+      ��  �� Property � �� �� Value$ Visible=�6S   6e   �� 5,       � ��$
 Auto=�6Q   6`   5-      .��5.      5/      51      52      