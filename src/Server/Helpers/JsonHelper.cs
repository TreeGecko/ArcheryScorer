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

        public static void PersistCompetition(Competition _jCompetition,
            ArcheryScorerManager _manager)
        {
            //Persist Competition
            Guid guid = ParseOrNewGuid(_jCompetition.Guid);

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

            //Persist Rounds
            foreach (Round jRound in _jCompetitionShooter.Rounds)
            {   
                PersistCompetitionShooter(guid, jRound, _manager);
            }
        }

        public static void PersistCompetitionShooter(Guid _competitionShooterGuid, 
            Round _jRound,
            ArcheryScorerManager _manager)
        {
            //Persist Round
            Guid guid = ParseOrNewGuid(_jRound.Guid);

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
        }


    }
}