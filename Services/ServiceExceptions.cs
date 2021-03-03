using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ExistingWishListItemInCollectionException : Exception
    {
        public ExistingWishListItemInCollectionException()
        {
        }
    }

    public class ExistingDiscInCollectionException : Exception
    {
        public ExistingDiscInCollectionException()
        {
        }
    }
}
