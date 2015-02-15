using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TreeGecko.Library.Archery.DAOs;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.AWS.Helpers;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Helpers;
using TreeGecko.Library.Net.Managers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Archery.Managers
{
    public class ArcheryScorerManager : AbstractCoreManagerWithUser
    {
        public ArcheryScorerManager() 
            : base("AS")
        {
        }

        #region Arrow

        public void Persist(Arrow _arrow)
        {
            ArrowDAO dao = new ArrowDAO(MongoDB);
            dao.Persist(_arrow);
        }

        public Arrow GetArrow(Guid _arrowGuid)
        {
            ArrowDAO dao = new ArrowDAO(MongoDB);
            return dao.Get(_arrowGuid);
        }

        public List<Arrow> GetArrows(Guid _endGuid)
        {
            ArrowDAO dao = new ArrowDAO(MongoDB);
            return dao.GetChildrenOf(_endGuid);
        } 

        #endregion

        #region Competition

        public void Persist(Competition _competition)
        {
            CompetitionDAO dao = new CompetitionDAO(MongoDB);
            dao.Persist(_competition);
        }

        public Competition GetCompetition(Guid _guid)
        {
            CompetitionDAO dao = new CompetitionDAO(MongoDB);
            return dao.Get(_guid);
        }

        public List<Competition> GetCompetitions()
        {
            CompetitionDAO dao = new CompetitionDAO(MongoDB);
            return dao.GetAll();
        }

        #endregion

        #region CompetitionFormat

        public void Persist(CompetitionFormat _competitionFormat)
        {
            CompetitionFormatDAO dao = new CompetitionFormatDAO(MongoDB);
            dao.Persist(_competitionFormat);
        }

        public List<CompetitionFormat> GetCompetitionFormats()
        {
            CompetitionFormatDAO dao = new CompetitionFormatDAO(MongoDB);
            return dao.GetAll();
        }

        public CompetitionFormat GetCompetitionFormat(Guid _guid)
        {
            CompetitionFormatDAO dao = new CompetitionFormatDAO(MongoDB);
            return dao.Get(_guid);
        }

        #endregion

        #region CompetitionShooter

        public void Persist(CompetitionShooter _competitionShooter)
        {
            CompetitionShooterDAO dao = new CompetitionShooterDAO(MongoDB);
            dao.Persist(_competitionShooter);
        }

        public List<CompetitionShooter> GetCompetitionShooters(Guid _competitionGuid)
        {
            CompetitionShooterDAO dao = new CompetitionShooterDAO(MongoDB);
            return dao.GetChildrenOf(_competitionGuid);
        }

        public CompetitionShooter GetCompetitionShooter(Guid _guid)
        {
            CompetitionShooterDAO dao = new CompetitionShooterDAO(MongoDB);
            return dao.Get(_guid);
        }

        #endregion

        #region End

        public void Persist(End _end)
        {
            EndDAO dao = new EndDAO(MongoDB);
            dao.Persist(_end);
        }

        public End GetEnd(Guid _endGuid)
        {
            EndDAO dao = new EndDAO(MongoDB);
            return dao.Get(_endGuid);
        }

        public List<End> GetEnds(Guid _roundGuid)
        {
            EndDAO dao = new EndDAO(MongoDB);
            return dao.GetChildrenOf(_roundGuid);
        }

        #endregion

        #region Organization

        public void Persist(Organization _organization)
        {
            OrganizationDAO dao = new OrganizationDAO(MongoDB);
            dao.Persist(_organization);
        }

        public Organization GetOrganization(Guid _guid)
        {
            OrganizationDAO dao = new OrganizationDAO(MongoDB);
            return dao.Get(_guid);
        }

        public List<Organization> GetOrganizations()
        {
            OrganizationDAO dao = new OrganizationDAO(MongoDB);
            return dao.GetAll();
        }

        #endregion

        #region Round

        public void Persist(Round _round)
        {
            RoundDAO dao = new RoundDAO(MongoDB);
            dao.Persist(_round);
        }

        public Round GetRound(Guid _guid)
        {
            RoundDAO dao = new RoundDAO(MongoDB);
            return dao.Get(_guid);
        }

        public List<Round> GetRounds(Guid _competitionShooterGuid)
        {
            RoundDAO dao = new RoundDAO(MongoDB);
            return dao.GetChildrenOf(_competitionShooterGuid);
        }

        #endregion

        #region Shooter

        public void Persist(Shooter _shooter)
        {
            ShooterDAO dao = new ShooterDAO(MongoDB);
            dao.Persist(_shooter);
        }

        public Shooter GetShooter(Guid _guid)
        {
            ShooterDAO dao = new ShooterDAO(MongoDB);
            return dao.Get(_guid);
        }

        public List<Shooter> GetShooters()
        {
            ShooterDAO dao = new ShooterDAO(MongoDB);
            return dao.GetAll();
        }

        #endregion

        public bool SendCannedEmail(TGUser _tgUser,
                                    string _cannedEmailName,
                                    NameValueCollection _additionParameters)
        {
            try
            {
                CannedEmail cannedEmail = GetCannedEmail(_cannedEmailName);

                if (cannedEmail != null)
                {
                    SystemEmail email = new SystemEmail(cannedEmail.Guid);

                    TGSerializedObject tgso = _tgUser.GetTGSerializedObject();
                    foreach (string key in _additionParameters.Keys)
                    {
                        string value = _additionParameters.Get(key);
                        tgso.Add(key, value);
                    }

                    CannedEmailHelper.PopulateEmail(cannedEmail, email, tgso);

                    SESHelper.SendMessage(email);
                    Persist(email);

                    return true;
                }

                TraceFileHelper.Warning("Canned email not found");
            }
            catch (Exception ex)
            {
                TraceFileHelper.Exception(ex);
            }

            return false;
        }
    }
}
