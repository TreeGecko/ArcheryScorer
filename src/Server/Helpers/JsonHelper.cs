using System;
using System.Collections.Generic;
using TreeGecko.Archery.Server.JsonObjects;
using TreeGecko.Library.Archery.Managers;

namespace TreeGecko.Archery.Server.Helpers
{
    public static class JsonHelper
    {
        private static Guid ParseOrNewGuid(string _guidString)
        {
            Guid guid;

            if (!Guid.TryParse(_guidString, out guid))
            {
                return Guid.NewGuid();
            }

            return guid;
        }

        public static List<Shooter> ConvertShooters(List<Library.Archery.Objects.Shooter> _shooters)
        {
            List<Shooter> jShooters = new List<Shooter>();

            foreach (Library.Archery.Objects.Shooter shooter in _shooters)
            {
                Shooter jShooter = new Shooter(shooter);
                jShooters.Add(jShooter);
            }

            return jShooters;
        }

        public static void PersistCompetition(Guid _accountGuid, 
            Competition _jCompetition,
            ArcheryScorerManager _manager)
        {
            //Persist Competition
            Guid guid = ParseOrNewGuid(_jCompetition.Guid);
            Library.Archery.Objects.Competition competition = _manager.GetCompetition(guid);
            if (competition == null)
            {
                competition = new Library.Archery.Objects.Competition
                {
                    Guid = guid,
                    Active = true,
                    ParentGuid = _accountGuid
                };
            }
            competition.ArrowsPerEnd = _jCompetition.ArrowsPerEnd;

            DateTime dateTime;
            if (DateTime.TryParse(_jCompetition.Date, out dateTime))
            {
                competition.Date = dateTime;
            }
            else
            {
                competition.Date = DateTime.Now.Date;
            }

            competition.Ends = _jCompetition.Ends;
            competition.MaxPointsPerArrow = _jCompetition.MaxPointsPerArrow;
            competition.Name = _jCompetition.Name;
            competition.Organization = _jCompetition.Organization;
            competition.Rounds = _jCompetition.Rounds;
            competition.TrackX = _jCompetition.TrackX;
            _manager.Persist(competition);

            //PersistCompetitionShoots
            foreach (CompetitionShooter jCompetitionShooter in _jCompetition.CompetitionShooters)
            {
                PersistCompetitionShooter(guid, jCompetitionShooter, _manager);
            }   
        }

        public static void PersistCompetitionShooter(Guid _competitionGuid, 
            CompetitionShooter _jCompetitionShooter,
            ArcheryScorerManager _manager)
        {
            //Persist Competition Shooters
            Guid guid = ParseOrNewGuid(_jCompetitionShooter.Guid);
            Library.Archery.Objects.CompetitionShooter shooter = _manager.GetCompetitionShooter(guid);

            if (shooter == null)
            {
                shooter = new Library.Archery.Objects.CompetitionShooter
                {
                    Active = true,
                    Guid = guid,
                    ParentGuid = _competitionGuid
                };
            }

            shooter.Notes = _jCompetitionShooter.Notes;

            Guid shooterGuid;
            if (Guid.TryParse(_jCompetitionShooter.ShooterGuid, out shooterGuid))
            {
                shooter.ShooterGuid = shooterGuid;
            }

            shooter.Target = _jCompetitionShooter.Target;
            _manager.Persist(shooter);

            //Persist Rounds
            foreach (Round jRound in _jCompetitionShooter.Rounds)
            {   
                PersistRound(guid, jRound, _manager);
            }
        }

        public static void PersistRound(Guid _competitionShooterGuid, 
            Round _jRound,
            ArcheryScorerManager _manager)
        {
            //Persist Round
            Guid guid = ParseOrNewGuid(_jRound.Guid);
            Library.Archery.Objects.Round round = _manager.GetRound(guid);

            if (round == null)
            {
                round = new Library.Archery.Objects.Round
                {
                    Active = true,
                    Guid = guid,
                    ParentGuid = _competitionShooterGuid
                };
            }

            round.Notes = _jRound.Notes;
            round.RoundScore = _jRound.RoundScore;
            round.Sequence = _jRound.Sequence;
            _manager.Persist(round);

            //Persist Ends
            foreach (End jEnd in _jRound.Ends)
            {
                PersistEnd(guid, jEnd, _manager);
            }
        }

        public static void PersistEnd(Guid _roundGuid, 
            End _jEnd,
            ArcheryScorerManager _manager)
        {
            //Persist End
            Guid guid = ParseOrNewGuid(_jEnd.Guid);
            Library.Archery.Objects.End end = _manager.GetEnd(guid);
            if (end == null)
            {
                end = new Library.Archery.Objects.End
                {
                    Active = true, 
                    Guid = _roundGuid, 
                    ParentGuid = _roundGuid
                };
            }
            end.EndScore = _jEnd.EndScore;
            end.Notes = _jEnd.Notes;
            end.Sequence = _jEnd.Sequence;
            _manager.Persist(end);

            //Persist Arrows
            foreach (Arrow jArrow in _jEnd.Arrows)
            {
                PersistArrow(guid, jArrow, _manager);
            }
        }

        public static void PersistArrow(Guid _endGuid,
            Arrow _jArrow,
            ArcheryScorerManager _manager)
        {
            //Persist Arrow
            Guid guid = ParseOrNewGuid(_jArrow.Guid);
            Library.Archery.Objects.Arrow arrow = _manager.GetArrow(guid);
            if (arrow == null)
            {
                arrow = new Library.Archery.Objects.Arrow
                {
                    Active = true, 
                    Guid = guid, 
                    ParentGuid = _endGuid
                };
            }
            arrow.IsX = _jArrow.IsX;
            arrow.Score = _jArrow.Score;

            _manager.Persist(arrow);

        }


    }
}