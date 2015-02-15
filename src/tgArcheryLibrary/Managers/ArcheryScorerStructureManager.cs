using System;
using TreeGecko.Library.Archery.DAOs;
using TreeGecko.Library.Net.Enums;
using TreeGecko.Library.Net.Managers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Archery.Managers
{
    public class ArcheryScorerStructureManager : AbstractCoreStructureManager
    {
        public ArcheryScorerStructureManager()
            : base("AS")
        {
        }

        public override void BuildDB()
        {
            BuildDB(true);

            ArrowDAO arrowDAO = new ArrowDAO(MongoDB);
            arrowDAO.BuildTable();

            CompetitionDAO competitionDAO = new CompetitionDAO(MongoDB);
            competitionDAO.BuildTable();

            CompetitionFormatDAO competitionFormatDAO = new CompetitionFormatDAO(MongoDB);
            competitionFormatDAO.BuildTable();

            CompetitionShooterDAO competitionShooterDAO = new CompetitionShooterDAO(MongoDB);
            competitionShooterDAO.BuildTable();

            EndDAO endDAO = new EndDAO(MongoDB);
            endDAO.BuildTable();

            OrganizationDAO organizationDAO = new OrganizationDAO(MongoDB);
            organizationDAO.BuildTable();

            RoundDAO roundDAO = new RoundDAO(MongoDB);
            roundDAO.BuildTable();

            ShooterDAO shooterDAO = new ShooterDAO(MongoDB);
            shooterDAO.BuildTable();
        }

        public static void BuildEmail()
        {
            ArcheryScorerManager manager = new ArcheryScorerManager();

            CannedEmail resetPasswordEmail = new CannedEmail
            {
                Active = true,
                BodyType = EmailBodyType.HTML,
                Description = "Sent when a user needs to have their password reset",
                From = "noreply@ngtdi.com",
                Guid = new Guid("c86b8e9d-ad6f-438e-8b7f-aecaee820d0d"),
                Name = "Reset Password Email",
                ReplyTo = "noreply@ngtdi.com",
                Subject = "NGTDI Password Reset",
                To = "[[EmailAddress]]",
                Body =
                    "<p>We have recieved a request to reset your password to the NGTDI system.</p><p>Your username is [[Username]].</p><p>Your password has been changed to [[Password]].</p>"
            };
            manager.Persist(resetPasswordEmail);

            CannedEmail emailAddressValidateEmail = new CannedEmail
            {
                Active = true,
                BodyType = EmailBodyType.HTML,
                Description = "Sent when a user needs to verify their email address",
                From = "noreply@ngtdi.com",
                Guid = new Guid("5eb9d4d2-1ec4-4d65-b45f-3e6d1547725c"),
                Name = "Validate Email Address",
                ReplyTo = "noreply@ngtdi.com",
                Subject = "NGTDI Email Validation",
                To = "[[EmailAddress]]",
                Body =
                    "<p>Please click the following link to complete your setup as a NGTDI user. <a href=\"[[SystemUrl]]/emailvalidation/[[ValidationText]]\">Validate my Email</a></p>"
            };
            manager.Persist(emailAddressValidateEmail);
        }
    }
}
