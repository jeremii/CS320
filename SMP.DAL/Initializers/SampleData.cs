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
            return new List<Post>()
            {
                new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now
                },
                new Post()
                {
                    UserId = user.Id,
                    Text = SampleText(user.FirstName),
                    Time = DateTime.Now
                }
            };
        }
        private static User MakeUser(string first, string last)
        {
            string emailPrefix = "@WVUP.EDU";
            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = first, LastName = last,
                Email = first.Substring(0, 1).ToLower() + last.ToLower() + emailPrefix.ToLower(),
                NormalizedEmail = first.Substring(0, 1).ToUpper() + last.ToUpper() + emailPrefix,
                EmailConfirmed = true
            };
            user.Posts = (List<Post>)GetPosts(user);
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test#1");
            return user;
        }
        public static IEnumerable<Follow> GetFollows(List<User> users)
        {
            List<Follow> follows = new List<Follow>();

            follows.Add(MakeFollow(users[0], users[1]));
            follows.Add(MakeFollow(users[0], users[2]));
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
            if (name == "Brady")
            {
                return "Brady's post";
            }
            else if (name == "Ethan")
            {
                return "Ethan's post";
            }
            else if (name == "Jeremi")
            {
                return "Jeremi's post";
            }
            else
            {
                return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eleifend turpis facilisis metus commodo scelerisque. Nullam facilisis tortor eu metus ornare, ac finibus elit efficitur. Vivamus sed tincidunt enim. Aliquam ut libero nec mauris ullamcorper interdum in nec tellus. Integer purus ex, dapibus et vehicula nec, imperdiet vitae turpis. Mauris sem odio, imperdiet vitae lacinia in, fermentum vel diam. Aliquam et ante blandit, imperdiet sem quis, condimentum est. Sed feugiat, justo dapibus ullamcorper blandit, urna purus dignissim neque, quis vestibulum nisl leo et urna. Maecenas molestie suscipit varius. Nunc ut pulvinar ligula. Etiam sapien ex, interdum non rutrum quis, porttitor commodo purus. Nunc quis ex sit amet ex finibus ullamcorper aliquam quis elit. Morbi nec efficitur purus.";
            }
        }

    }
}