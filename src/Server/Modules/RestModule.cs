using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Newtonsoft.Json;
using TreeGecko.Archery.Server.Helpers;
using TreeGecko.Archery.Server.JsonObjects;
using TreeGecko.Library.Archery.Managers;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Net.Objects;
using Competition = TreeGecko.Library.Archery.Objects.Competition;

namespace TreeGecko.Archery.Server.Modules
{
    public class RestModule : NancyModule
    {
        private readonly ArcheryScorerManager m_Manager = new ArcheryScorerManager();

        public RestModule()
        {
            Post["/rest/login"] = _parameters =>
            {
                Response response = HandleLoginPost(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/shooters"] = _parameters =>
            {
                Response response = HandleShootersGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/organizations"] = _parameters =>
            {
                Response response = HandleGetOrganizations(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/shooter/{guid}"] = _parameters =>
            {
                Response response = HandleShooterGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Post["/rest/shooter"] = _parameters =>
            {
                Response response = HandleShooterPost(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/guids/{quantity}"] = _parameters =>
            {
                Response response = HandleGuidGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/competition/{guid}"] = _parameters =>
            {
                Response response = HandleCompetitionGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Post["/rest/competition"] = _parameters =>
            {
                Response response = HandleCompetitionPost(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/competitions"] = _parameters =>
            {
                Response response = HandleCompetitionsGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };
        }

        private string HandleGetOrganizations(DynamicDictionary _parameters)
        {
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    var organizations = m_Manager.GetOrganizations();

                    var jOrganizations = JsonHelper.ConvertOrganizations(organizations);
                    return JsonConvert.SerializeObject(jOrganizations);
                }

                return null;
            }

            return null;
        }

        private string HandleCompetitionsGet(DynamicDictionary _parameters)
        {
            CompetitionsResult result = new CompetitionsResult { Result = "Failure" };
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    List<Competition> competitions = m_Manager.GetCompetitions(account.Guid);
                    
                    List<CompetitionRow> rows = new List<CompetitionRow>();
                    result.CompetitionRows = rows;

                    foreach (Competition competition in competitions)
                    {
                        string organizationName = "";

                        if (competition.OrganizationGuid != null)
                        {
                            var org = m_Manager.GetOrganization(competition.OrganizationGuid.Value);
                            if (org != null)
                            {
                                organizationName = org.Name;
                            }
                        }

                        CompetitionRow row = new CompetitionRow
                        {
                            Guid = competition.Guid.ToString(),
                            Date = competition.Date.ToString("d"),
                            Name = competition.Name,
                            Organization = organizationName
                        };

                        rows.Add(row);
                    }
                }
            }

            return JsonConvert.SerializeObject(result);
        }

        private string HandleCompetitionPost(DynamicDictionary _parameters)
        {
            BaseResult result = new BaseResult { Result = "Failure" };
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    string json = ReadBody();
                    if (!string.IsNullOrEmpty(json))
                    {
                        var jCompetition = JsonConvert.DeserializeObject<JsonObjects.Competition>(json);

                        if (jCompetition != null)
                        {
                            JsonHelper.PersistCompetition(account.Guid, jCompetition, m_Manager);

                            result.Result = "Success";
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(result);
        }

        private string HandleGuidGet(DynamicDictionary _parameters)
        {
            TGUser user;

            List<string> guidList = new List<string>();

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                string temp = _parameters["quantity"];
                int quantity;

                if (!int.TryParse(temp, out quantity))
                {
                    quantity = 100;
                }

                if (quantity > 1000)
                {
                    quantity = 1000;
                }

                for (int i = 0; i < quantity; i++)
                {
                    Guid guid = Guid.NewGuid();
                    guidList.Add(guid.ToString());
                }

                return JsonConvert.SerializeObject(guidList);
            }

            return null;
        }

        private string HandleShooterPost(DynamicDictionary _parameters)
        {
            BaseResult result = new BaseResult {Result = "Failure"};
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    string json = ReadBody();
                    if (!string.IsNullOrEmpty(json))
                    {
                        var jShooter = JsonConvert.DeserializeObject<JsonObjects.Shooter>(json);

                        if (jShooter != null)
                        {
                            Library.Archery.Objects.Shooter shooter = null;

                            if (!string.IsNullOrEmpty(jShooter.Guid))
                            {
                                Guid shooterGuid;
                                if (Guid.TryParse(jShooter.Guid, out shooterGuid))
                                {
                                    shooter = m_Manager.GetShooter(shooterGuid);

                                    if (!account.Guid.Equals(shooter.ParentGuid))
                                    {
                                        //For some reason the shooter doesn't belong to the account.
                                        shooter = null;
                                    }
                                }
                            }

                            if (shooter == null)
                            {
                                shooter = new Library.Archery.Objects.Shooter
                                {
                                    Active = true,
                                    ParentGuid = account.Guid
                                };
                            }

                            shooter.FirstName = jShooter.FirstName;
                            shooter.LastName = jShooter.LastName;

                            DateTime bDate;
                            if (DateTime.TryParse(jShooter.BirthDate, out bDate))
                            {
                                shooter.BirthDate = bDate;
                            }

                            m_Manager.Persist(shooter);
                            result.Result = "Success";
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(result);
        }

        private string HandleShooterGet(DynamicDictionary _parameters)
        {
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    Guid shooterGuid;
                    if (Guid.TryParse(_parameters["guid"], out shooterGuid))
                    {
                        var shooter = m_Manager.GetShooter(shooterGuid);

                        if (shooter != null
                            && account.Guid.Equals(shooter.ParentGuid))
                        {
                            var jShooter = new JsonObjects.Shooter(shooter);
                            return JsonConvert.SerializeObject(jShooter);
                        }
                    }
                }

                return null;
            }

            return null;
        }

        private string HandleCompetitionGet(DynamicDictionary _parameters)
        {
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null)
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    Guid competitionGuid;
                    if (Guid.TryParse(_parameters["guid"], out competitionGuid))
                    {
                        var competition = m_Manager.GetCompetition(competitionGuid);

                        if (competition != null
                            && account.Guid.Equals(competition.ParentGuid))
                        {
                            var jCompetition = new JsonObjects.Competition(competition, m_Manager);
                            return JsonConvert.SerializeObject(jCompetition);
                        }
                    }
                }

                return null;
            }

            return null;
        }

        private string HandleShootersGet(DynamicDictionary _parameters)
        {
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null )
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    var shooters = m_Manager.GetShooters(account.Guid);

                    var jShooters = JsonHelper.ConvertShooters(shooters);
                    return JsonConvert.SerializeObject(jShooters);
                }

                return null;
            }

            return null;
        }

        private string HandleLoginPost(DynamicDictionary _parameters)
        {
            string json = ReadBody();
            LoginRequest jLoginRequest = JsonConvert.DeserializeObject<LoginRequest>(json);

            if (jLoginRequest != null)
            {
                TGUser user;

                LoginResult jResult = AuthHelper.Authorize(m_Manager,
                    jLoginRequest.Username, jLoginRequest.Password, out user);

                return JsonConvert.SerializeObject(jResult);
            }

            return null;
        }
        

        private string ReadBody()
        {
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                string json = sr.ReadToEnd();
                return json;
            }

            return null;
        }

        

    }
}