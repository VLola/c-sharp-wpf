
# Project 79.

Using Azure.Blob to implement a manager for cloud storage of files with the ability to
- adding files
- deleting files
- getting a list of files
- view text files

For graphics use WPF + MVVM

In the list of files when you click on a specific one - download it

Implement partial match search

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_79/Resources/Menu_Main.png)

___

# Project 53.

In accordance with the layout, implement the layout of the Telegram.

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_53/Resources/Menu_Main.png)

___
# Project 52.

In accordance with the layout, implement the layout of the audio player.

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_52/Resources/Menu_Main.png)

___

# Project 51.

In accordance with the layout, implement the layout cake order form.

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_51/Resources/Menu_Main.png)

___

# Project 50.

Implement the layout of the Windows 10 calculator.

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_50/Resources/Menu_Calculator.png)

___
# Project 49.

Implement the output of goods from the database in WPF. 

Use UserControl for a specific product. 

Implement search and CRUD architecture in the application. 

The data source must be MsSql. 

Process data from C# using Entity Framework.

Source: yellow.ua

Menu main:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Main.png)

Catalog:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Catalog.png)

Hot Category:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Hot_Category1.png)

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Hot_Category2.png)

Novelty:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Novelty.png)

Shares:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Shares.png)

List product:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_List_Product.png)

Product filter:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Filter.jpg)

Footer:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Footer.png)

Authorization:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Authorization.png)

Registration:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Registration.png)

Add and delete product:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-wpf/master/Project_49/Resources/Menu_Add_Delete_Product.png)

___
# Project 40.

Develop the Tamagotchi app.

The life cycle of a character is 1-2 minutes.
The character randomly issues requests (but the same request is not issued in a row).

Requests can be the following: Feed, Walk, Put to bed, Treat, Play.
If requests are not granted three times,
the character "gets sick" and asks him to be treated.
If it fails, it "dies".

The character is displayed in the console window using pseudo-graphics.

The dialogue with the character is carried out by calling the Show() method of the MessageBox class from the System.Windows.Forms namespace.

For more information on working with this method, contact your instructor or MSDN.

To solve this problem, you will need the Timer class from the System.Timers namespace, whose event is Elapsed,
delegate type ElapsedEventHandler, occurs after a certain time interval,
which is set in the Interval property.

The Start() and Stop() methods start and stop the timer, respectively.

You may also want to pause the application,
in this case, you can call the Sleep() method of the Thread class from the System.

Threading namespace, transferring the required number of milliseconds to it.
___