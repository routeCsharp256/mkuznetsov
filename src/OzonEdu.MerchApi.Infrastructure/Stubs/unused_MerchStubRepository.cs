﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchAggregate;

namespace OzonEdu.MerchApi.Infrastructure.Stubs
{
    public class MerchStubRepository : IMerchRepository
    {
        private IEnumerable<Merch> _merches;

        public MerchStubRepository()
        {
            _merches = new List<Merch>
            {
                new Merch(
                    MerchType.WelcomePack,
                    1,
                    new IssueDate(new DateTime(2020, 10, 12)),
                    MerchStatus.Ready,
                    null),
                new Merch(
                    MerchType.ConferenceListenerPack,
                    1,
                    new IssueDate(new DateTime(2021, 11, 2)),
                    MerchStatus.Ready,
                    null),
                new Merch(
                    MerchType.VeteranPack,
                    2,
                    new IssueDate(new DateTime(2021, 10, 20)),
                    MerchStatus.Ready,
                    null),
                new Merch(
                    MerchType.ConferenceSpeakerPack,
                    2,
                    new IssueDate(new DateTime(2021, 11, 2)),
                    MerchStatus.Ready,
                    null),
                new Merch(
                    MerchType.ConferenceListenerPack,
                    3,
                    new IssueDate(new DateTime(2021, 11, 2)),
                    MerchStatus.Ready,
                    new Dictionary<Sku, Quantity>
                    {
                        {new Sku(23343), new Quantity(1)},
                        {new Sku(4534), new Quantity(2)}
                    }),
            };
        }
        
        public async Task<IEnumerable<Merch>> FindByEmployeeIdAsync(long employeeId, CancellationToken cancellationToken = default)
        {
            _merches = await FindAll(cancellationToken);
            
            var res = _merches.Where(m => m.EmployeeId == employeeId);

            return res;
        }

        public Task<IEnumerable<Merch>> FindAll(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_merches);
        }

        public async Task<bool> Insert(Merch merch, CancellationToken cancellationToken = default)
        {
            ((List<Merch>)_merches).Add(merch);

            return true;
        }
    }
}