using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class GuessingGameController : Controller
    {
        private const string SessionKeyNumber = "_Number";
        private const string SessionKeyAttempts = "_Attempts";

        [HttpGet("/GuessingGame")]
        public IActionResult Index()
        {
            var random = new Random();
            int number = random.Next(1, 101);
            HttpContext.Session.SetInt32(SessionKeyNumber, number);
            HttpContext.Session.SetInt32(SessionKeyAttempts, 0);

            return View();
        }

        [HttpPost("/GuessingGame")]
        public IActionResult Index(int? guess)
        {
            if (!guess.HasValue)
            {
                ViewData["Message"] = "Give a number.";
                return View();
            }

            int? sessionNumber = HttpContext.Session.GetInt32(SessionKeyNumber);
            if (!sessionNumber.HasValue)
            {
                
                sessionNumber = new Random().Next(1, 101);
                HttpContext.Session.SetInt32(SessionKeyNumber, sessionNumber.Value);
            }

            int number = sessionNumber.Value;
            int attempts = HttpContext.Session.GetInt32(SessionKeyAttempts).GetValueOrDefault() + 1;
            HttpContext.Session.SetInt32(SessionKeyAttempts, attempts);

            if (guess.Value == number)
            {
                ViewData["Message"] = $"congratulations! {attempts} attempts.";
                
                number = new Random().Next(1, 101);
                HttpContext.Session.SetInt32(SessionKeyNumber, number);
                HttpContext.Session.SetInt32(SessionKeyAttempts, 0);

                
                UpdateHighScores(attempts);
            }
            else if (guess.Value > number)
            {
                ViewData["Message"] = "Too high!";
            }
            else
            {
                ViewData["Message"] = "Too low! ";
            }

            ViewData["Attempts"] = attempts;
            ViewData["HighScores"] = GetHighScores();

            return View();
        }

        private void UpdateHighScores(int attempts)
        {
            var highScores = GetHighScores();
            highScores.Add(attempts);
            highScores.Sort();

            if (highScores.Count > 5)
            {
                highScores = highScores.GetRange(0, 5); 
            }

            string highScoresString = string.Join(",", highScores);
            Response.Cookies.Append("HighScores", highScoresString, new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            });
        }

        private List<int> GetHighScores()
        {
            var highScores = new List<int>();
            var highScoresString = Request.Cookies["HighScores"];

            if (!string.IsNullOrEmpty(highScoresString))
            {
                var scores = highScoresString.Split(',');

                foreach (var score in scores)
                {
                    if (int.TryParse(score, out int parsedScore))
                    {
                        highScores.Add(parsedScore);
                    }
                }
            }

            return highScores;
        }
    }
}
