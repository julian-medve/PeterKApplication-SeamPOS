using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using PeterKApplication.Models;
using Xamarin.Forms;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Used in walk-through pages. Looks like grid of circles with text inside of them 
    /// </summary>
    public partial class ChoiceRings : ContentView
    {
        public static readonly BindableProperty SingleChoiceProperty =
            BindableProperty.Create("SingleChoice", typeof(bool), typeof(ChoiceRings), false);

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create("Items",
            typeof(List<ChoiceRingItemGroup>), typeof(ChoiceRings), new List<ChoiceRingItemGroup>(),
            BindingMode.TwoWay);

        private int _fullHeight;

        public ChoiceRings()
        {
            InitializeComponent();

            ItemTapped = new Command<ChoiceRingItem>(ItemTappedExecuted);

            FullHeight = 120;
        }

        private void ItemTappedExecuted(ChoiceRingItem obj)
        {
            if (obj == null) return;
            Items = new List<ChoiceRingItemGroup>(Items.Select(i =>
            {
                if (i.Item1 == obj)
                {
                    i.Item1.Selected = !i.Item1.Selected;
                }
                else if (SingleChoice && i.Item1 != null)
                {
                    i.Item1.Selected = false;
                }
                
                if (i.Item2 == obj)
                {
                    i.Item2.Selected = !i.Item2.Selected;
                }
                else if (SingleChoice && i.Item2 != null)
                {
                    i.Item2.Selected = false;
                }
                
                if(i.Item3 == obj)
                {
                    i.Item3.Selected = !i.Item3.Selected;
                }
                else if (SingleChoice && i.Item3 != null)
                {
                    i.Item3.Selected = false;
                }

                return i;
            }).ToList());
        }

        /// <summary>
        /// Should there only be one possible selection?, default is false
        /// </summary>
        public bool SingleChoice
        {
            get => (bool) GetValue(SingleChoiceProperty);
            set => SetValue(SingleChoiceProperty, value);
        }

        /// <summary>
        /// List items, please use it
        /// </summary>
        public List<ChoiceRingItemGroup> Items
        {
            get => (List<ChoiceRingItemGroup>) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public ICommand ItemTapped { get; }

        public int FullHeight
        {
            get => _fullHeight;
            set
            {
                _fullHeight = value;
                OnPropertyChanged(nameof(FullHeight));
                OnPropertyChanged(nameof(WiderHeight));
                OnPropertyChanged(nameof(SmallerHeight));
                OnPropertyChanged(nameof(ABitSmallerHeight));
            }
        }

        public int WiderHeight => FullHeight - 45;
        public int SmallerHeight => 45;
        public int ABitSmallerHeight => FullHeight - 20;
    }
}