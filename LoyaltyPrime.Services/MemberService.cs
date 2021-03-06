﻿using LoyaltyPrime.Core;
using LoyaltyPrime.Core.Models;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace LoyaltyPrime.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all members
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable> GetAllMembersAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new member
        /// </summary>
        /// <param name="member"></param>
        public async Task<int> AddMemberAsync(Member member)
        {
            return await this._unitOfWork.Members.AddMemberAsync(member);
        }
    }
}