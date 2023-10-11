using System;
using System.Collections.Generic;
using System.Windows;

namespace RpgCharacterGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    List<Player> _allCharacters = new();

    int _currentId = 0;


    List<string> Classes = new()
    {
        "Fighter",
        "Wizard"
    };


    public MainWindow()
    {
        InitializeComponent();

        // Lägg in våra classes-strängar i ComboBoxen

        cbSelectRole.ItemsSource = Classes;


    }

    private void cbSelectRole_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        // Kolla om vi har selectad Fighter eller Wizard
        string selectedRole = (string)cbSelectRole.SelectedItem;

        // Byt content i label för speciall stats till rätt namn
        if (selectedRole == "Fighter")
        {
            lblStats.Content = "Armor";
        }
        else if (selectedRole == "Wizard")
        {
            lblStats.Content = "Mana";
        }

        // Visa labelm för speciell stat och dess inputruta
        lblStats.Visibility = Visibility.Visible;
        txtStats.Visibility = Visibility.Visible;



    }

    private void btnRoleStats_Click(object sender, RoutedEventArgs e)
    {
        // Autogenerera strength 
        // Sätt strength

        txtStr.Text = GenerateStat().ToString();

        // Autogenerera intelligence
        // Sätt inteööigence 

        txtInt.Text = GenerateStat().ToString();
    }

    private int GenerateStat()
    {
        return new Random().Next(3, 19);
    }

    private bool ValidateStringConversion(string stringToValidate)
    {
        bool validateOk = int.TryParse(stringToValidate, out int result);

        return validateOk;
    }



    private void btnSaveCharacter_Click(object sender, RoutedEventArgs e)
    {
        string strength = txtStr.Text;
        string intelligence = txtInt.Text;
        string name = txtName.Text;
        string stat = txtStats.Text;
        string role = (string)cbSelectRole.SelectedItem;

        if (strength != "" && intelligence != "" && name != "" && stat != "" && role != "" && ValidateStringConversion(stat))
        {
            // Lägg till karaktären
            lstCharacter.Items.Add(new { Id = _currentId, Name = name, Strength = strength, Intelligence = intelligence, Role = role, Ability = stat });

            if (role == "Fighter")
            {
                // Göre en ny fighter 
                Fighter newFighter = new(_currentId, name, int.Parse(strength), int.Parse(intelligence), int.Parse(stat));

                _allCharacters.Add(newFighter);
            }

            else if (role == "Wizard")
            {
                Wizard newWizard = new(_currentId, name, int.Parse(strength), int.Parse(intelligence), int.Parse(stat));


                _allCharacters.Add(newWizard);
            }
        }
        else
        {
            // Visa varning 

            MessageBox.Show("Please add all the requierd information!", "Warning");


        }
    }
}
