﻿namespace Sube2.HelloMvc.Models
{
    public class Ogrenci : Object
    {
        public int Ogrenciid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Numara { get; set; }
        public List<Ders> Dersler { get; set; }

        //public override string ToString()
        //{
        //    return $"Ad:{this.Ad}-Soyad:{Soyad}-Numara:{Numara}";
        //}
    }
}
