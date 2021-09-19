using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkingWithDates.Data;
using WorkingWithDates.LanguageExtensions;
using WorkingWithDates.Models;

namespace WorkingWithDates.Classes
{
    public class DataOperations
    {
        public static Context Context = new();

        public static async Task<List<Person1>> People() 
            => await Task.Run(async () => await Context.Person1.ToListAsync());

        /// <summary>
        /// Demonstration showing how to perform a BETWEEN on dates
        /// with an additional condition on EventId
        /// </summary>
        public static List<Events> BetweenEventDates(DateTime startDate, DateTime endDate, int identifier) 
            => Context.Events
                .BetweenStartDate(startDate, endDate).Where(@event => @event.EventId == identifier)
                .ToList();


        /// <summary>
        /// Return a list of birthday records with a specific date range on <see cref="Birthdays.BirthDate"/>
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Task<List<Birthdays>> GetBirthdaysList(DateTime startDate, DateTime endDate) 
            => Task.Run(async () 
                => await Context.Birthdays.BirthDatesBetween(startDate, endDate)
                    .OrderBy(birthday => birthday.BirthDate).ToListAsync());

        /// <summary>
        /// Load people into a BindingList with no where conditions
        /// </summary>
        /// <returns></returns>
        public static async Task<BindingList<Person1>> PeopleLocal() 
            => await Task.Run(async () =>
            {
                await Context.Person1.LoadAsync();
                return Context.Person1.Local.ToBindingList();
            });

        /// <summary>
        /// Get changes into a string if there are changes. Will not show deleted recordss
        /// </summary>
        /// <returns></returns>
        public static string ShowEventsChangesPlain()
        {
            StringBuilder builder = new();

            foreach (var person in Context.Person1.Local)
            {
                if (Context.Entry(person).State != EntityState.Unchanged)
                {
                    builder.AppendLine($"{person.Id} {person.FirstName} {person.LastName} {Context.Entry(person).State}");
                }
            }

            return builder.ToString();
        }
    }
}
