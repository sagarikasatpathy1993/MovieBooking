using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MyShowBooking.Models;
using System.Collections;
using System.IO;

namespace MyShowBooking.Models
{
    public class MovieInformation
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ToString());
        
        public void RegisterUser(Models.UserDetails userdetails)
        {
            string query = "insert into UserDetails (FirstName,LastName,Email,Password,MobileNumber,UserType)" +
                "Values(@FirstName,@LastName,@Email,@Password,@MobileNumber,@UserType)";
            connection.Execute(query, new { userdetails.FirstName, userdetails.LastName, userdetails.Email, userdetails.Password, userdetails.MobileNumber, userdetails.UserType });
        }

        public IEnumerable<City> CityNameList()
        {
            string query = "select  CityID, CityName from City";
            var result = connection.Query<City>(query);
            return result;
        }


        public IEnumerable<State> StateNameList()
        {
            string query = "Select StateID,StateName from State";
            var result = connection.Query<State>(query);
            return result;
        }

        


        public IEnumerable<Movie> GetMovieDropdown(int city)
        {

          string query ="SELECT MovieID,MovieName from Movie where"+
              " ScreenID IN ( select ScreenID from Screen where"+
              " TheaterID IN ( select TheaterID from TheaterDetails where"+
              " AreaID IN ( select AreaID from Area Where CityID=@city)))";
          var result = connection.Query<Movie>(query, new { city });
          return result;
            
        }


        public IEnumerable<City> GetCityDropDown(int state)
        {
            string query = "select CityName from City where StateID in (select StateID from State where StateID=@state)";
            var result = connection.Query<City>(query, new { state });
            return result;
        }

        
        public int GetMovieID(string movie)
        {
            connection.Open();
            SqlCommand query = new SqlCommand("select MovieID from Movie where MovieName='" + movie+ "'", connection);
            int result = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();
            return result;
        }

        public string StateName(int stateid)
        {
            connection.Open();
            SqlCommand query = new SqlCommand("select StateName from State where StateID='" + stateid + "'", connection);
           string result = Convert.ToString(query.ExecuteScalar());
            connection.Close();
            return result;
        }

        public string CityName(int cityid)
        {
            connection.Open();
            SqlCommand query = new SqlCommand("select CityName from City where cityID='" + cityid + "'", connection);
            string result = Convert.ToString(query.ExecuteScalar());
            connection.Close();
            return result;
        }

        public string AreaName(int areaid)
        {
            connection.Open();
            SqlCommand query = new SqlCommand("select AreaName from area where AreaID='" + areaid + "'", connection);
            string result = Convert.ToString(query.ExecuteScalar());
            connection.Close();
            return result;
        }


        public int GetScheduleID(string date)
        {
            connection.Open();
            var convertedDate=Convert.ToDateTime(date);
            SqlCommand query = new SqlCommand("select ScheduleID from Schedule where Date='" + convertedDate + "'", connection);
            int result = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();
            return result;
        }


        public int GetCityID(string city)
        {
            connection.Open();
            SqlCommand query = new SqlCommand("select  CityID from City Where CityName='" + city + "'", connection);
            int result = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();
            return result;
        }


        public IEnumerable<Schedule> GetDateDropDown(int movie)
        {
            MovieBooking moviebook=new MovieBooking();
            string MovieName = moviebook.MovieName;
          
            string query = "select Date from Schedule where MovieID In(select MovieID from Movie where MovieID=@movie)";

            var result = connection.Query<Schedule>(query, new { movie });
            return result;
        }
      

        public IEnumerable<Area> AreaNameList()
        {
            string query = "select AreaID, AreaName from Area";
            var result = connection.Query<Area>(query);
            return result;
        }
        
        public IEnumerable<Movie> MovieNameList()
        {
            string query = "select  MovieID, MovieName from Movie";
            var result = connection.Query<Movie>(query);
            return result;
        }

     
        public IEnumerable<Schedule> DateList()
        {
            string query = "select  ScheduleID,Date From Schedule";
            var result = connection.Query<Schedule>(query);
            return result;
        }

        
        public IEnumerable<TheaterDetails> CinemaNameList()
        {
            string query = "select TheaterID,Name from TheaterDetails";
            var result = connection.Query<TheaterDetails>(query);
            return result;
        }

        
        public string checkUser(Models.Login login)
        {
         
      
            string Name = login.Email;
            string Pass = login.Password;
          
            connection.Open();
            SqlCommand query = new SqlCommand("select UserType from UserDetails where Email='"+ Name+"'and Password='"+Pass+"'",connection);
            string result = Convert.ToString(query.ExecuteScalar());
            return result;
          
        }

       
        /*public void MovieRegister(Models.MovieRegistration movie)
        {
               
                
               
                string state = "if NOT Exists (select StateName from State where StateName=@StateName)"+
                                " insert into State(StateName) values(@StateName)";
                    
                        connection.Execute(state, new { movie.StateName });


                 string city = "if Not Exists (select CityName from City where CityName=@CityName)"+
                                " insert into City(StateID,CityName) values((select StateID from State where StateName=@StateName),@CityName)";
                
                        connection.Execute(city, new { movie.CityName, movie.StateName, movie.StateID });

                string area = "if NOT EXISTS (select AreaName from Area where AreaName=@AreaName)"
                             +" insert into Area(CityID,AreaName) values((select CityID from City where CityName=@CityName),@AreaName)";
                        
                        connection.Execute(area, new { movie.AreaName, movie.CityName, movie.CityID });

                string query = "if Not Exists (select Name from TheaterDetails where Name=@Name)"+
                               " insert into TheaterDetails(AreaID,Name)" + "Values((select AreaID from Area where AreaName=@AreaName),@Name)";
                            
                        connection.Execute(query, new { movie.Name, movie.AreaName, movie.AreaID });

                string query2 = "if not exists(select ScreenName,TheaterID from Screen"+
                                " where ScreenName=@ScreenName and TheaterID=(select TheaterID from TheaterDetails where Name=@Name)) " +
                                " insert into Screen(TheaterID,ScreenName)" +
                                " Values((select TheaterID from TheaterDetails where Name=@Name),@ScreenName)";
                        
                        connection.Execute(query2, new { movie.TheaterID, movie.ScreenName, movie.Name });


                string query5 = "if not exists(select SeatName  from SeatType where SeatName=@SeatName "+
                                " and ScreenID=(select ScreenID from Screen inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)"+
                                " where Name=@Name  and ScreenName=@ScreenName))"+
                                " insert into SeatType(ScreenID,SeatName,NumberOfSeats,Price)" +
                                " values((select ScreenID from Screen inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)"+
                                " where Name=@Name and ScreenName=@ScreenName ),@SeatName,@NumberOfSeats,@Price)";
                
                        connection.Execute(query5, new { movie.SeatName,movie.ScreenName, movie.NumberOfSeats, movie.Price, movie.ScreenID, movie.TheaterID, movie.Name });


                string query3 = "if not exists(select MovieName from Movie where MovieName=@MovieName"+
                                " and ScreenID=(select ScreenID from Screen"+
                                " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID) where Name=@Name and ScreenName=@ScreenName))"+
                                "insert into Movie(ScreenID,MovieName) values((select ScreenID from Screen inner join TheaterDetails "+
                                " on(Screen.TheaterID=TheaterDetails.TheaterID) where Name=@Name and ScreenName=@ScreenName),@MovieName)";
                
                connection.Execute(query3, new { movie.MovieName, movie.ScreenName, movie.ScreenID, movie.Name, movie.TheaterID });

                
                string query4 = "if not Exists(select Date from Schedule where Date=@Date"+
                                 " and MovieID=(select MovieID from Movie "+
                                  "  inner join Screen on(Movie.ScreenID=Screen.ScreenID)"+
                                   " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)"+
                                  " where Name=@Name and ScreenName=@ScreenName and MovieName=@MovieName ))"+"insert into Schedule(MovieID,Date)"+
                                  " Values((select MovieID from Movie inner join Screen on(Movie.ScreenID=Screen.ScreenID)"+
                                   " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)"+
                                   " where Name=@Name and ScreenName=@ScreenName and MovieName=@MovieName),@Date)";

                connection.Execute(query4, new { movie.Date, movie.MovieName,movie.MovieID ,movie.ScreenName,movie.Name});

                string query6 = "if not Exists(select StartTime,EndTime from ShowTime where StartTime=@StartTime and EndTime=@EndTime "+
                                " and ScheduleID=(select ScheduleID from Schedule inner join Movie on(Schedule.MovieID=Movie.MovieID)"+
                                 " inner join Screen on(Movie.ScreenID=Screen.ScreenID)"+
                                 " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)"+
                                 "  where Name=@Name and ScreenName=@ScreenName and MovieName=@MovieName))" +
                                 " insert into ShowTime(ScheduleID,StartTime,EndTime)"+
                                  " values((select ScheduleID from Schedule inner join Movie on(Schedule.MovieID=Movie.MovieID)"+
                                  " inner join Screen on(Movie.ScreenID=Screen.ScreenID)"+
                                  " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)"+
                                  " where Name=@Name and ScreenName=@ScreenName and MovieName=@MovieName),@StartTime,@EndTime)";

                connection.Execute(query6, new { movie.ScheduleID, movie.StartTime, movie.EndTime,movie.Date,movie.ScreenName,movie.MovieName,movie.Name });
            
            
         }*/


        public void StateRegistration(Models.MovieRegistration movie)
        {
            
            string state="if NOT Exists (select StateName from State where StateName=@StateName)"+
                                " insert into State(StateName) values(@StateName)";
            connection.Execute(state, new { movie.StateName });

            
            string city = "if Not Exists (select CityName from City where CityName=@CityName)" +
                                " insert into City(StateID,CityName) values((select StateID from State where StateName=@StateName),@CityName)";

            connection.Execute(city, new { movie.CityName, movie.StateName, movie.StateID });

            string area = "if NOT EXISTS (select AreaName from Area where AreaName=@AreaName)"
                            + " insert into Area(CityID,AreaName) values((select CityID from City where CityName=@CityName),@AreaName)";

            connection.Execute(area, new { movie.AreaName, movie.CityName, movie.CityID });

        }


        public void TheaterRegistration(string theater,int state,int city,int area)
        {
            string query = "if Not Exists (select Name,AreaID from TheaterDetails where Name=@theater and AreaID=@area)" +
                "insert into TheaterDetails(Name,AreaID) values(@theater,@area)";
                            

            connection.Execute(query, new { theater,state,city,area });
        }

       /* public string insertImage(MovieImages movie)
        {
            string img =
                    "insert into Image(ImageName,Url,MovieID) values(@ImageName,@Url,@MovieID))";
            connection.Execute(img, new { movie.ImageName, movie.Url, movie.MovieID });
            return "inserted";
        }
        
        public string  GetFileName()
        {
            string query = "select ImageName from Image where MovieID=2";
            var result =connection.Query<string>(query);
            return "inserted";
        }*/

       
        public IEnumerable<TheaterDetails> FindTheaterName(int state,string city,string movie,string date)
        {
            string query = "select  TheaterDetails.TheaterID,Name from Schedule " +
                            "inner join Movie on(Schedule.MovieID=Movie.MovieID) and MovieName=@movie " +
                            "inner join Screen on(Movie.ScreenID=Screen.ScreenID)" +
                            "inner join TheaterDetails on(TheaterDetails.TheaterID=Screen.TheaterID)" +
                            "inner join Area on (TheaterDetails.AreaID=Area.AreaID)" +
                            "inner join City on(Area.CityID=City.CityID) " +
                            "inner join State on(City.StateID=State.StateID)" +
                            "where State.StateID=@state and Date=@date and CityName=@city";
            
            
                var result = connection.Query<TheaterDetails>(query, new { state, city, movie, date });
                return result;
         }


      

        public IEnumerable<ShowTime> GetTimeDropDown(int theater,string movie,string date)
        {
            var datevalue = Convert.ToDateTime(date);
            string query=" select StartTime from ShowTime "+
                        " inner join Schedule on(ShowTime.ScheduleID=Schedule.ScheduleID)"+
                            "inner join Movie on(Schedule.MovieID=Movie.MovieID)  " +
                            "inner join Screen on(Movie.ScreenID=Screen.ScreenID)" +
                            "inner join TheaterDetails on(TheaterDetails.TheaterID=Screen.TheaterID)"+
                            " where TheaterDetails.TheaterID=@theater and MovieName=@movie and Date=@datevalue  ";
            var result=connection.Query<ShowTime>(query,new { theater,movie,datevalue });
            return result;

        }


        public int GetTimeID(string time)
        {
            connection.Open();
            var timeval = Convert.ToDateTime(time);
            SqlCommand query = new SqlCommand("select  TimeID from ShowTime Where StartTime='" + timeval + "'", connection);
            int result = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();
            return result;
        }


        public int GetPrice(int theater,string seat,int movie)
        {
            connection.Open();
            SqlCommand query =new SqlCommand( "select Price from SeatType" +
                          "  inner join Screen on(SeatType.ScreenID=Screen.ScreenID)" +
                          "  inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                          "  inner join Movie on(Screen.ScreenID=Movie.ScreenID)" +
                          "  where   SeatName='" + seat +" 'and TheaterDetails.TheaterID='"+ theater
                          +" 'and MovieID='"+movie+"'",connection);
            int result = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();
            return result;
        }


        public IEnumerable<SeatType> GetSeatList(int time,string movie,string date) 
        {
           
            var datevalue=Convert.ToDateTime(date);
            string query = "select SeatName,StartTime,Date,MovieName From SeatType " +
                           " inner join Screen on(SeatType.ScreenID=Screen.ScreenID)" +
                            "inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                         "inner join Movie on(Movie.ScreenID=Screen.ScreenID)" +
                         "inner join Schedule on(Movie.MovieID=Schedule.MovieID)" +
                         "inner join ShowTime on(Schedule.ScheduleID=ShowTime.ScheduleID)" +
                         "where  TimeID=@time and date=@datevalue and MovieName=@movie";
            var result = connection.Query<SeatType>(query, new { time, movie, datevalue });
            return result;
        }

      
        public int GetUserID(string email)
        {
            connection.Open();
           
            SqlCommand query = new SqlCommand("select UserID from UserDetails where Email='" + email + "'", connection);
            int result = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();
            return result;
        }
        

        public string GetTheaterName(int theater)
        {
            connection.Open();

            SqlCommand query = new SqlCommand("select Name from TheaterDetails where TheaterID='" + theater + "'", connection);
            string result = Convert.ToString(query.ExecuteScalar());
            connection.Close();
            return result;

        }


        public void InsertIntoBooking(int movieid,int scheduleid,int TheaterList,string TimeList,string SeatList,int Quantity,int amount,int userid)
        {
            string query = "insert into BookingDetails(MovieID,ScheduleID,NumberOfSeats,AmountPaid,UserID)" +
                         " values(@movieid,@scheduleid,@Quantity,@amount,@userid)";
            connection.Execute(query, new { movieid, scheduleid, TheaterList, Quantity, amount, userid });
                        
        }

        public bool IsTheaterExist(string theater,int state,int city,int area)
        {
            connection.Open();
            SqlCommand query = new SqlCommand("select TheaterID from TheaterDetails where AreaID='" + area + "'and Name='" + theater + "'", connection);
            bool result = Convert.ToBoolean(query.ExecuteScalar());
           
            connection.Close();
            return result;

        }
      
        public  void MovieRegistration(string theatername,string city,string area ,string state,MovieRegistration movie)
        {
            string screen = "if not exists(select ScreenName,TheaterID from Screen" +
                                " where ScreenName=@ScreenName and TheaterID=(select TheaterID from TheaterDetails where Name=@theatername)) " +
                                " insert into Screen(TheaterID,ScreenName)" +
                                " Values((select TheaterID from TheaterDetails where Name=@theatername),@ScreenName)";

            connection.Execute(screen, new {  movie.ScreenName, theatername });

            string seats = "if not exists(select SeatName  from SeatType where SeatName=@SeatName " +
                                " and ScreenID=(select ScreenID from Screen inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                                " where Name=@theatername  and ScreenName=@ScreenName))" +
                                " insert into SeatType(ScreenID,SeatName,NumberOfSeats,Price)" +
                                " values((select ScreenID from Screen inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                                " where Name=@theatername and ScreenName=@ScreenName ),@SeatName,@NumberOfSeats,@Price)";

            connection.Execute(seats, new { movie.SeatName, movie.ScreenName, movie.NumberOfSeats, movie.Price, theatername, movie.TheaterID, movie.Name });

            string query3 = "if not exists(select MovieName from Movie where MovieName=@MovieName" +
                               " and ScreenID=(select ScreenID from Screen" +
                               " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID) where Name=@theatername and ScreenName=@ScreenName))" +
                               "insert into Movie(ScreenID,MovieName) values((select ScreenID from Screen inner join TheaterDetails " +
                               " on(Screen.TheaterID=TheaterDetails.TheaterID) where Name=@theatername and ScreenName=@ScreenName),@MovieName)";

            connection.Execute(query3, new { movie.MovieName, movie.ScreenName, movie.ScreenID, movie.Name, movie.TheaterID, theatername });

            string query4 = "if not Exists(select Date from Schedule where Date=@Date" +
                                 " and MovieID=(select MovieID from Movie " +
                                  "  inner join Screen on(Movie.ScreenID=Screen.ScreenID)" +
                                   " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                                  " where Name=@theatername and ScreenName=@ScreenName and MovieName=@MovieName ))" + "insert into Schedule(MovieID,Date)" +
                                  " Values((select MovieID from Movie inner join Screen on(Movie.ScreenID=Screen.ScreenID)" +
                                   " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                                   " where Name=@theatername and ScreenName=@ScreenName and MovieName=@MovieName),@Date)";

            connection.Execute(query4, new { movie.Date, movie.MovieName, movie.MovieID, movie.ScreenName, movie.Name, theatername });

            string query6 = "if not Exists(select StartTime,EndTime from ShowTime where StartTime=@StartTime and EndTime=@EndTime " +
                               " and ScheduleID=(select ScheduleID from Schedule inner join Movie on(Schedule.MovieID=Movie.MovieID)" +
                                " inner join Screen on(Movie.ScreenID=Screen.ScreenID)" +
                                " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                                "  where Name=@theatername and ScreenName=@ScreenName and MovieName=@MovieName))" +
                                " insert into ShowTime(ScheduleID,StartTime,EndTime)" +
                                 " values((select ScheduleID from Schedule inner join Movie on(Schedule.MovieID=Movie.MovieID)" +
                                 " inner join Screen on(Movie.ScreenID=Screen.ScreenID)" +
                                 " inner join TheaterDetails on(Screen.TheaterID=TheaterDetails.TheaterID)" +
                                 " where Name=@theatername and ScreenName=@ScreenName and MovieName=@MovieName),@StartTime,@EndTime)";

            connection.Execute(query6, new { movie.ScheduleID, movie.StartTime, movie.EndTime, movie.Date, movie.ScreenName, movie.MovieName, movie.Name, theatername });
            
        }

        
    }
}