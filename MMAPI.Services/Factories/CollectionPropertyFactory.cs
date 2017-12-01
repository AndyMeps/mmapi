using MMAPI.Services.Attributes;
using MMAPI.Services.Interfaces;
using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace MMAPI.Services.Factories
{
    public static class CollectionPropertyFactory
    {
        public static string GetCollectionName<T>(CultureInfo culture = null) where T : IDocumentEntity
        {
            // First try to get the name from the CollectionName attribute.
            var collectionNameAttr = typeof(T).FirstAttribute<CollectionNameAttribute>();
            if (collectionNameAttr != null)
            {
                return collectionNameAttr.Name;
            }

            // Otherwise, use the name of the class and pluralize it.
            return Pluralize(typeof(T).Name.ToLower(), culture);
        }

        private static string Pluralize(string word, CultureInfo culture = null)
        {
            var ps = PluralizationService.CreateService(culture ?? new CultureInfo("en-US"));
            return ps.Pluralize(word);
        }
    }
}
