using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_eshop_xincunli.Models
{
    public enum SearchType
    {
        Artist,
        Album,
        Song
    }
    public interface ISearch
    {
       SearchType SearchType { get; }
    }
}