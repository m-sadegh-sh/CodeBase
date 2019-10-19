namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class QueuedMailActionProvider : LogActionProviderBase<QueuedMail> {
        protected override void BuildCore(QueuedMail old,
                                          QueuedMail @new,
                                          LogAction action) {
            WithInstance(action.Snapshots,
                         old,
                         @new).
                    AddIfChanged(qm => qm.Id).
                    AddIfChanged(qm => qm.From).
                    AddIfChanged(qm => qm.FromName).
                    AddIfChanged(qm => qm.To).
                    AddIfChanged(qm => qm.ToName).
                    AddIfChanged(qm => qm.Cc).
                    AddIfChanged(qm => qm.Bcc).
                    AddIfChanged(qm => qm.Subject).
                    AddIfChanged(qm => qm.Body).
                    AddIfChanged(qm => qm.IsBodyHtml).
                    AddIfChanged(qm => qm.SendPriority).
                    AddIfChanged(qm => qm.SendingTries).
                    AddIfChanged(qm => qm.SentDateUtc).
                    AddIfChanged(qm => qm.AccountConfigId);
        }
    }
}