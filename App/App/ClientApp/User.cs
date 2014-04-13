using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class User
    {
        private String username;
        private String passw;
        private int points;
        private int group;

        public void setUsername(String _username)
        {
            username = _username;
        }

        public String getUsername()
        {
            return username;
        }

        public void setPassw(String _passw)
        {
            passw = _passw;
        }

        public String getPassw()
        {
            return passw;
        }

        public void setPoints(int _points)
        {
            points = _points;
        }

        public int getPoints()
        {
            return points;
        }

        public void setGroup(int _group)
        {
            group = _group;
        }

       
            
    }
}
