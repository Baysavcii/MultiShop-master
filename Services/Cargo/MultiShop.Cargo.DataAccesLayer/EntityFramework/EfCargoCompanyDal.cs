﻿using MultiShop.Cargo.DataAccesLayer.Abstract;
using MultiShop.Cargo.DataAccesLayer.Concrete;
using MultiShop.Cargo.DataAccesLayer.Repositories;
using MultiShop.Cargo.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccesLayer.EntityFramework
{
    public class EfCargoCompanyDal: GenericRepository<CargoCompany>, ICargoCompanyDal
    {
        public EfCargoCompanyDal(CargoContext context) : base(context) { }
    }
}
