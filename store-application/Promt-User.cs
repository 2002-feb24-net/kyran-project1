using System;
using System.Collections;
using System.Collections.Generic;

namespace Store
{
     public class promptUser
    {

        public enum choice
        {
            New_Customer = 1,
            Place_Order = 2,
            Store_Locations = 3,
            Search_Customer = 4,
            Quit = 5
        };

        public void promtUserMenu(choice uChoice)
        {
            switch(uChoice)
            {
                case choice.New_Customer:
                    break;

                case choice.Place_Order:
                    break;

                case choice.Store_Locations:
                    break;

                case choice.Search_Customer:
                    break;

                case choice.Quit:
                    this.Close();
                    break;
            }
        }

    }
}
