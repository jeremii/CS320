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
        public static IEnumerable<User> GetUsers2() => new List<User>
        {
            new User {Email = "erhodes8@wvup.edu", NormalizedEmail = "ERHODES8@WVUP.EDU", SecurityStamp = Guid.NewGuid().ToString()},
        };

        public static IEnumerable<Post> GetPosts2(List<User> Users) => new List<Post>
        {
            new Post { UserId = Users.Where( x => x.Email == "erhodes8@wvup.edu").FirstOrDefault().Id, Text = "Test"}
        };
        public static IList<Follow> GetFollows2( List<User> users)
        {
            var follows = new List<Follow>();

            for(int a = 0; a < users.Count; a++ )
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if( a != i )
                    {
                        Follow follow = MakeFollow(users[a], users[i]);
                        follows.Add(follow);
                        Console.WriteLine(a.ToString() + " follows " + i.ToString());
                    }
                }
            }
            return follows;
        }
        public static IEnumerable<Post> GetPosts(User user)
        {
            TimeSpan ts0 = DateTime.Now.AddHours(6) - DateTime.Now;
            TimeSpan ts1 = DateTime.Now.AddHours(12) - DateTime.Now;
            TimeSpan ts2 = DateTime.Now.AddHours(24) - DateTime.Now;
            TimeSpan ts3 = DateTime.Now.AddHours(36) - DateTime.Now;
            return new List<Post>()
            {
                new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts0)
                },
                new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts1)
                },
                new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts2)
                },
                new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now.Subtract(ts3)
                }
            };
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
                    Url = "http://www.msnbc.com/feeds/latest"
                };
                feeds.Add(feed);
                feed = new Rss()
                {
                    UserId = user.Id,
                    Url = "https://www.huffingtonpost.com/section/front-page/feed"
                };
                feeds.Add(feed);
                feed = new Rss()
                {
                    UserId = user.Id,
                    Url = "http://feeds.washingtonpost.com/rss/politics"
                };
                feeds.Add(feed);
            }
            return feeds;
        }
        public static IEnumerable<Message> MakeThread(User sender, User receiver)
        {
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
                            Text = i.ToString()
                        });
                }
                else
                {
                    messages.Add(
                        new Message()
                        {
                            SenderId = receiver.Id,
                            ReceiverId = sender.Id,
                            Text = i.ToString()
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
            user.EduExp = "<ul><li>1985 - Washington Universtiy</li><li>1987 - Ohio State University</li></ul>";
            user.JobExp = "<ul><li>2010 - Groupon</li><li>2015 - Google</li></ul>";
            user.PicturePath = "Happy_smiley_face.png";
            user.Posts = (List<Post>)GetPosts(user);
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test#1");
            //user.RSSFeeds = (List<Rss>)GetRssFeeds(user);
            return user;
        }
        public static IEnumerable<Follow> GetFollows(List<User> users)
        {
            List<Follow> follows = new List<Follow>();

            follows.Add(MakeFollow(users[0], users[1]));
            //follows.Add(MakeFollow(users[0], users[2]));
            follows.Add(MakeFollow(users[1], users[0]));
            follows.Add(MakeFollow(users[1], users[2]));
            follows.Add(MakeFollow(users[2], users[0]));
            follows.Add(MakeFollow(users[2], users[1]));

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

            users.Add(MakeUser("Brady", "Starcher"));
            users.Add(MakeUser("Ethan", "Rhodes"));
            users.Add(MakeUser("Jeremi", "Swann"));

            return users;
        }
        public static string SampleText(string name = "")
        {
            string suffix = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eleifend turpis facilisis metus commodo scelerisque. Nullam facilisis tortor eu metus ornare, ac finibus elit efficitur. ";
            return name + " " + suffix;
        }

    }
}