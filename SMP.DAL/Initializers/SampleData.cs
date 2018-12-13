using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SMP.Models.Entities;
using SMP.DAL.EF;
using Microsoft.AspNetCore.Identity;


namespace SMP.DAL.Initializers
{
    public static class SampleData
    {

        public static IEnumerable<Post> GetPosts(List<User> users)
        {
            TimeSpan ts0 = DateTime.Now.AddHours(0) - DateTime.Now;
            TimeSpan ts1 = DateTime.Now.AddHours(24) - DateTime.Now;
            TimeSpan ts2 = DateTime.Now.AddHours(48) - DateTime.Now;
            TimeSpan ts3 = DateTime.Now.AddHours(72) - DateTime.Now;
            List<Post> posts = new List<Post>();
            Post post;
            foreach (User user in users)
            {

                post = new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts0)
                };
                posts.Add(post);
                post = new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts1)
                };
                posts.Add(post);
                post = new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts2)
                };
                posts.Add(post);
                post = new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts3)
                };
                posts.Add(post);
            }
            return posts;
        }
        public static IEnumerable<Rss> GetRss(IList<User> users)
        {
            List<Rss> feeds = new List<Rss>();
            Rss feed = new Rss();
            foreach (User user in users)
            {
                feed = new Rss()
                {
                    UserId = user.Id,
                    Url = "http://feeds.washingtonpost.com/rss/world"
                };
                feeds.Add(feed);
                feed = new Rss()
                {
                    UserId = user.Id,
                    Url = "http://feeds.washingtonpost.com/rss/rss_innovations"
                };
                feeds.Add(feed);
            }
            return feeds;
        }
        public static IEnumerable<Message> MetaMakeThread( List<User> users)
        {
            List<Message> messages = new List<Message>();
            for (int x = 0; x < users.Count; x++ )
            {
                for (int y = 0; y < users.Count; y++)
                {
                    if( x != y )
                    {
                        messages.AddRange(MakeThread(users[x], users[y]));
                    }
                }
            }
            return messages;
        }
        public static IEnumerable<Message> MakeThread(User sender, User receiver)
        {
            string dummyText1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            string dummyText2 = "Proin eleifend turpis facilisis metus commodo scelerisque.";
            IList<Message> messages = new List<Message>();
            for (int i = 0; i < 10; i++)
            {
                if( i%2 == 0)
                {
                    messages.Add(
                        new Message()
                        {
                            SenderId = sender.Id,
                            ReceiverId = receiver.Id,
                            Text = dummyText1
                        });
                }
                else
                {
                    messages.Add(
                        new Message()
                        {
                            SenderId = receiver.Id,
                            ReceiverId = sender.Id,
                            Text = dummyText2
                        });
                }
            }
            return messages;
        }
        private static User MakeUser(string first, string last)
        {
            string emailSuffix = "@wvup.edu";
            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = first, LastName = last,
                Email = first.Substring(0, 1).ToLower() + last.ToLower() + emailSuffix,
                NormalizedEmail = first.Substring(0, 1).ToUpper() + last.ToUpper() + emailSuffix.ToUpper(),
                UserName = first.Substring(0,1).ToLower() + last.ToLower(),
                NormalizedUserName = first.Substring(0, 1).ToUpper() + last.ToUpper(),
                EmailConfirmed = true
            };
            user.Bio = "Let's do this!";
            user.EduExp = "<ul><li><b>2018 - WVU-P</b> Associate's in Computer Science</li><li><b>2020 - WVU-P</b> Bachelor's Software Engineering</li></ul>";
            user.JobExp = "<ul><li><b>2012 - Acme café</b></li><li><b>2016 - Acme corp.</b></li></ul>";
            user.PicturePath = "random guy.jpeg";
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test#1");
            //user.RSSFeeds = (List<Rss>)GetRssFeeds(user);
            return user;
        }
        public static IEnumerable<Follow> GetFollows(List<User> users)
        {
            // Returning object
            List<Follow> follows = new List<Follow>();

            // Random variable for not following some users.
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int x = 0; x < users.Count; x++ )
            {
                // Create random user indices to not follow for each user.
                int random0 = rand.Next(2, users.Count/2);
                int random1 = rand.Next(0, users.Count - 1);
                int random2 = rand.Next(0, users.Count - 1);
                int random3 = rand.Next(0, users.Count - 1);
                int random4 = rand.Next(0, users.Count - 1);
                int random5 = rand.Next(0, users.Count - 1);

                for (int y = 0; y < users.Count; y++ )
                {
                    // Don't follow self, 
                    // Don't follow every other random user, and
                    // Don't follow 5 random users.
                    if (y == x ||
                        y % random0 == 0 ||
                        y == random1 || 
                        y == random2 || 
                        y == random3 || 
                        y == random4 || 
                        y == random5 )
                    {
                        continue;
                    }
                    else
                    {
                        follows.Add(MakeFollow(users[x], users[y]));
                    }
                }
            }
            return follows;
        }
        private static Follow MakeFollow(User userIs, User following)
            => new Follow()
            {
                UserId = userIs.Id,
                FollowerId = following.Id
            };
        public static IEnumerable<User> GetUsers() 
        {
            List<User> users = new List<User>();

            users.Add(MakeUser("John", "Doe"));
            users.Add(MakeUser("Jane", "Jones"));
            users.Add(MakeUser("Joe", "Schmoe"));
            users.Add(MakeUser("James", "Starkey"));
            users.Add(MakeUser("Robert", "Philips"));
            users.Add(MakeUser("Lilly", "Abrams"));
            users.Add(MakeUser("John", "Little"));
            users.Add(MakeUser("Melissa", "Castle"));
            users.Add(MakeUser("James", "Lyon"));
            users.Add(MakeUser("Mandy", "Quest"));
            users.Add(MakeUser("Jane", "Jefferson"));
            users.Add(MakeUser("Don", "Hill"));
            users.Add(MakeUser("Derrick", "Long"));
            users.Add(MakeUser("Rasalee", "Porath"));
            users.Add(MakeUser("Loraine", "Heinemann"));
            users.Add(MakeUser("Harley", "Hawke"));
            users.Add(MakeUser("Larry", "Durrett"));
            users.Add(MakeUser("Hong", "Dumont"));
            users.Add(MakeUser("Alycia", "Aguilar"));
            users.Add(MakeUser("Breann", "Dales"));
            users.Add(MakeUser("Kenia", "Lilly"));
            users.Add(MakeUser("Violeta", "Seiber"));
            users.Add(MakeUser("Kyla", "Pinion"));
            users.Add(MakeUser("Susanne", "Fleurant"));
            users.Add(MakeUser("Joellen", "Sergio"));
            users.Add(MakeUser("Sergio", "Leon"));
            users.Add(MakeUser("Milan", "Marotta"));
            users.Add(MakeUser("Maribel", "Sloan"));
            users.Add(MakeUser("Layla", "Jeffery"));
            users.Add(MakeUser("Burt", "Uribe"));
            users.Add(MakeUser("Cleo", "Brice"));
            users.Add(MakeUser("Noel", "Faver"));
            users.Add(MakeUser("Audie", "Accardi"));
            users.Add(MakeUser("Moses", "Ship"));
            users.Add(MakeUser("Johnny", "Basic"));

            return users;
        }
        public static string SampleText(string name = "")
        {
            string suffix = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eleifend turpis facilisis metus commodo scelerisque. Nullam facilisis tortor eu metus ornare, ac finibus elit efficitur. ";
            return name + " " + suffix;
        }

    }
}