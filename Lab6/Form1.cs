using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Lab6.Models;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private BindingList<Restaurant> _restaurants;

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            _restaurants = new BindingList<Restaurant>();

            
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
                var restaurants = db.Restaurants.ToList();
                foreach (var restaurant in restaurants)
                {
                    _restaurants.Add(restaurant);
                }
            }

            
            listBox1.DataSource = _restaurants;
            listBox1.DisplayMember = "Name"; 

            
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Restaurant selectedRestaurant)
            {
                
                MessageBox.Show($"Вибрано ресторан: {selectedRestaurant.Name}");
            }
        }

        
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newRestaurant = new Restaurant
            {
                Name = "New Restaurant", 
                Address = "Some Address",
                AverageBill = 25.50M
            };

            using (var db = new AppDbContext())
            {
                db.Restaurants.Add(newRestaurant);
                db.SaveChanges();
                _restaurants.Add(newRestaurant); 
            }
        }

        
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Restaurant selectedRestaurant)
            {
                using (var db = new AppDbContext())
                {
                    db.Restaurants.Remove(selectedRestaurant);
                    db.SaveChanges();
                    _restaurants.Remove(selectedRestaurant); 
                }
            }
        }

        
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Restaurant selectedRestaurant)
            {
                selectedRestaurant.Name = "Updated Restaurant";
                using (var db = new AppDbContext())
                {
                    db.Restaurants.Update(selectedRestaurant);
                    db.SaveChanges();
                }
                listBox1.Refresh(); 
            }
        }
    }
}
