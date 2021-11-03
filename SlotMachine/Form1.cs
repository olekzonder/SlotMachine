using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace SlotMachine
{
    public partial class Form1 : Form
    {
        Image apple = Properties.Resources.apple;
        Image cherry = Properties.Resources.cherry;
        Image grapes = Properties.Resources.grapes;
        Image lemon = Properties.Resources.lemon;
        Image orange = Properties.Resources.orange;
        Image plum = Properties.Resources.plum;
        Image melon = Properties.Resources.melon;
        Image seven = Properties.Resources.seven;
        int secret = 0;
        int secret_enabled = 0;
        int wins = 0;
        SoundPlayer sound = new SoundPlayer();
        SoundPlayer loop = new SoundPlayer();
        int balance = 100; //Исходный баланс.
        int counter_money = 0; //Текущий ставка.
        int counter_try = 0; //Счетчик попыток.
        int win_money = 0; //Выигранные деньги.
        bool IsActive = true; //Активность кнопки "Погнали!"
        Random random = new Random();
        int attract;
        int lost = 0;
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }
        public void dvg1_Tick(object sender, EventArgs e)
        {
            int dvg;
            if (balance <= 100) dvg = random.Next(8); // Получаем случайное число от 0-7
            else dvg = random.Next(100) % 8;
            if (attract > 0)
            {
                dvg = dvg % 5;
            }
            label1.Text = dvg.ToString(); //Выводим полученное число
            switch (dvg)
            {
                case 0:
                    pictureBox1.Image = apple;
                    break;
                case 1:
                    pictureBox1.Image = cherry;
                    break;
                case 2:
                    pictureBox1.Image = grapes;
                    break;
                case 3:
                    pictureBox1.Image = lemon;
                    break;
                case 4:
                    pictureBox1.Image = orange;
                    break;
                case 5:
                    pictureBox1.Image = plum;
                    break;
                case 6:
                    pictureBox1.Image = melon;
                    break;
                case 7:
                    pictureBox1.Image = seven;
                    break;
            }
        }
        public void dvg2_Tick(object sender, EventArgs e)
        {
            int dvg;
            if (balance <= 100 || wins == 0) dvg = random.Next(8); // Получаем случайное число от 0-7
            else dvg = (random.Next(100) - wins) % 8;
            if (attract > 0)
            {
                dvg = dvg % 5;
            }
            label2.Text = dvg.ToString();
            switch (dvg)
            {
                case 0:
                    pictureBox2.Image = apple;
                    break;
                case 1:
                    pictureBox2.Image = cherry;
                    break;
                case 2:
                    pictureBox2.Image = grapes;
                    break;
                case 3:
                    pictureBox2.Image = lemon;
                    break;
                case 4:
                    pictureBox2.Image = orange;
                    break;
                case 5:
                    pictureBox2.Image = plum;
                    break;
                case 6:
                    pictureBox2.Image = melon;
                    break;
                case 7:
                    pictureBox2.Image = seven;
                    break;
            }
        }
        public void dvg3_Tick(object sender, EventArgs e)
        {
            int dvg;
            if (balance <= 100) dvg = random.Next(8); // Получаем случайное число от 0-7
            else dvg = random.Next(100) % 7 + 1;
            if (attract > 0)
            {
                attract--;
                dvg = dvg % 5;
            }
            label3.Text = dvg.ToString();
            switch (dvg)
            {
                case 0:
                    pictureBox3.Image = apple;
                    break;
                case 1:
                    pictureBox3.Image = cherry;
                    break;
                case 2:
                    pictureBox3.Image = grapes;
                    break;
                case 3:
                    pictureBox3.Image = lemon;
                    break;
                case 4:
                    pictureBox3.Image = orange;
                    break;
                case 5:
                    pictureBox3.Image = plum;
                    break;
                case 6:
                    pictureBox3.Image = melon;
                    break;
                case 7:
                    pictureBox3.Image = seven;
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
                sound.Stream = Properties.Resources.play;
                sound.PlaySync();
                loop.Stream = Properties.Resources.tick;
                loop.PlayLooping();
                //активируем таймеры
                dvg1.Enabled = true;
                dvg2.Enabled = true;
                dvg3.Enabled = true;
                stop1.Enabled = true;
                stop2.Enabled = true;
                stop3.Enabled = true;
                IsActive = true;
                button1.Enabled = false; //Пока барабаны крутятся кнопка "Погнали!"  заблокирована.}
        }
        private void stop1_Tick(object sender, EventArgs e)
        {
            dvg1.Enabled = false; //Останавливаем таймер запускающий первый барабан.
            stop1.Enabled = false; //Останавливаем таймер останавливающий первый барабан.
            if (secret_enabled == 1)
            {
                label1.Text = "7";
                pictureBox1.Image = Properties.Resources.seven;
            }
        }
        private void stop2_Tick(object sender, EventArgs e)
        {
            dvg2.Enabled = false;
            stop2.Enabled = false;
            if (secret_enabled == 1)
            {
                label2.Text = "7";
                pictureBox2.Image = Properties.Resources.seven;
            }
        }
        private void stop3_Tick(object sender, EventArgs e)
        {
            counter_try--;
            loop.Stop();
            dvg3.Enabled = false;
            stop3.Enabled = false;
            if (secret_enabled == 1)
            {
                label3.Text = "7";
                pictureBox3.Image = Properties.Resources.seven;
            }
            loop.Stop();
            
            Win_Money();
            if (IsActive)
            {
                if (balance < 100) lost++;
                if (lost == 3 && balance <= 50)
                {
                    attract = random.Next(2, 4);
                }
                if (counter_try != 0) //Если число попыток больше 0, то даем возможность нажать на кнопку "Погнали!" еще раз, если нет, то блокируем кнопку "Погнали!", и выводим информационное окно.
                {

                    label6.Text = "Осталось попыток: " + counter_try;
                    button1.Enabled = true;
                }
                else if(balance > 0 )
                {
                    label6.Text = "Осталось попыток: " + counter_try;
                    MessageBox.Show("Делайте новую ставку!", "Попытки закончились...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (numericUpDown1.Value < balance) { button2.Enabled = true; }

                }
                else
                {
                    DialogResult gameover = MessageBox.Show("Нажмите на ОК, чтобы продать свою душу за $100", "Игра окончена...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (gameover == DialogResult.OK)
                    {
                        numericUpDown1.Value = 25;
                        balance = 100;
                        button1.Text = "Погнали!";
                        label4.Text = "Баланс: $" + balance;
                        label6.Text = "Осталось попыток: 0";
                        button2.Enabled = true;
                    }
                }
            }
            if(counter_try > 0)
            {
                button1.PerformClick();
            }
        }
        private void Init_Counter(decimal counter)
        {
            counter_money = Convert.ToInt32(counter);
            balance = balance - counter_money;
            label4.Text = "Баланс: $" + balance;
            counter_try = (int)numericUpDown1.Value / 25;
            if (counter_try > 1) button1.Text = "Автоигра!";
            else button1.Text = "Погнали!";
            label6.Text = "Осталось попыток: " + counter_try;
        }

        private void Win_Money()
        {
            if (label1.Text == "0" && label2.Text == "0" && label3.Text == "0") Upd_Win_Money(17);
            else if ((label1.Text == "0" && label2.Text == "0") || (label2.Text == "0" && label3.Text == "0")) Upd_Win_Money(7);
            if (label1.Text == "1" && label2.Text == "1" && label3.Text == "1") Upd_Win_Money(10);
            else if ((label1.Text == "1" && label2.Text == "1") || (label2.Text == "1" && label3.Text == "1")) Upd_Win_Money(1);
            if (label1.Text == "2" && label2.Text == "2" && label3.Text == "2") Upd_Win_Money(11);
            else if ((label1.Text == "2" && label2.Text == "2") || (label2.Text == "2" && label3.Text == "2")) Upd_Win_Money(2);
            if (label1.Text == "3" && label2.Text == "3" && label3.Text == "3") Upd_Win_Money(12);
            else if ((label1.Text == "3" && label2.Text == "3") || (label2.Text == "3" && label3.Text == "3")) Upd_Win_Money(3);
            if (label1.Text == "4" && label2.Text == "4" && label3.Text == "4") Upd_Win_Money(13);
            else if ((label1.Text == "4" && label2.Text == "4") || (label2.Text == "4" && label3.Text == "4")) Upd_Win_Money(4);
            if (label1.Text == "5" && label2.Text == "5" && label3.Text == "5") Upd_Win_Money(14);
            else if ((label1.Text == "5" && label2.Text == "5") || (label2.Text == "5" && label3.Text == "5")) Upd_Win_Money(5);
            if (label1.Text == "6" && label2.Text == "6" && label3.Text == "6") Upd_Win_Money(15);
            else if ((label1.Text == "6" && label2.Text == "6") || (label2.Text == "6" && label3.Text == "6")) Upd_Win_Money(6);
            if (label1.Text == "7" && label2.Text == "7" && label3.Text == "7") Upd_Win_Money(20);
            else if ((label1.Text == "7" && label2.Text == "7") || (label2.Text == "7" && label3.Text == "7")) Upd_Win_Money(10);
        }
        private void Upd_Win_Money(int number)
        {
            wins++;
            lost = 0;
            attract = 0;
            if (number == 20)
            {
                sound.Stream = Properties.Resources.jackpot;
                sound.Play();
                DialogResult jackpot = MessageBox.Show("Вы выиграли: автомат по языкам программирования", "Джекпот!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                balance += 10000;
                label4.Text = "Баланс: $" + balance; //Выводим обновленный балансе
                button1.Enabled = false; //Блокируем кнопку "Погнали!"
                button2.Enabled = true; //Открываем кнопку "Сделать ставку"
                IsActive = false; //Это костыль, может кто-то предложит как от него отказаться ))
                if (jackpot == DialogResult.OK)
                {
                    MessageBox.Show("Делайте новую ставку!", "Новая игра", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    label6.Text = "Осталось попыток: 0"; // Скидываем оставшиеся попытки.
                }
            }
            else
            {
                win_money = counter_money * number; //умножаем ставку на коэффициент получаем кол-во выигранных денег
                sound.Stream = Properties.Resources.gotmoney;
                sound.PlaySync();
                DialogResult result = MessageBox.Show("Вы выиграли: $" + win_money, "Поздравляем!", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Выыодим поздравления.
                balance = balance + win_money; //Прибавляем выигрыш к балансу
                label4.Text = "Баланс: $" + balance; //Выводим обновленный балансе
                button1.Enabled = false; //Блокируем кнопку "Погнали!"
                if (numericUpDown1.Value <= balance) { button2.Enabled = true;}
                IsActive = false; //Это костыль, может кто-то предложит как от него отказаться ))
                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Делайте новую ставку!", "Новая игра", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    label6.Text = "Осталось попыток: 0"; // Скидываем оставшиеся попытки.
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sound.Stream = Properties.Resources.inserted;
            sound.PlaySync();
            Init_Counter(numericUpDown1.Value);
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (secret < 10) secret++;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (secret >= 10 && secret < 15) secret++;
            if (secret == 15)
            {
                sound.Stream = Properties.Resources.secret;
                sound.PlaySync();
                secret_enabled = 1;
                secret++;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value -= numericUpDown1.Value % 25;
            if (numericUpDown1.Value > balance) button2.Enabled = false;
            else if (label6.Text == "Осталось попыток: 0") button2.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            attract = random.Next(2, 4);
        }
    }
}