﻿using DatabaseLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public void AddUser(UserPostModel userPostModel);
    }
}
