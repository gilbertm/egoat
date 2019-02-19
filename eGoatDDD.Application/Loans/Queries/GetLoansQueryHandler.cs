using eGoatDDD.Application.Loans.Models;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Loans.Queries
{
    public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, LoansListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetLoansQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<LoansListViewModel> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            LoansListViewModel model = null;
            IEnumerable<Loan> loans = null;

            switch (request.Role)
            {
                case "Lessee":

                    loans = _context.Loans.Where(l => (l.LesseeId == request.UserId || l.LesseeId == null) && l.LoanDetail.Status > 0 && l.LoanDetail.Status < 200);
                    // TODO://
                    // a. check lesees allowable number of loan count AND loan amount
                    // b. (a) is based on rating on the system
                    // c. location based

                    // get all loans that are granted to lessee or unassigned 
                    // as long as the status is broascasted; on-going; non-finished
                    model = new LoansListViewModel
                    {
                        Loans = await _context.Loans
                        .Where(l => (l.LesseeId == request.UserId || l.LesseeId == null) && l.LoanDetail.Status > 0 && l.LoanDetail.Status < 200)
                        .Include(a => a.Applicants)
                            .ThenInclude(applicant => applicant.ApplicantLessee)
                        .OrderByDescending(l => l.LoanDetail.Status)
                        .Select(LoanDto.Projection)
                        .ToListAsync(cancellationToken),

                        CreateEnabled = false
                    };

                    break;
                case "Lender":

                    // TODO://
                    // a. check lenders allowable number of loan count AND loan amount
                    // b. (a) is based on the package subscription (SASS model)
                    // c. location based

                    // get all loans that are granted to lessee or unassigned 
                    // as long as the status is broascasted; on-going; non-finished
                    var tloans = _context.Applicants;
                    model = new LoansListViewModel
                    {
                        Loans = await _context.Loans
                        .Where(l => (l.LoanDetail.LenderId == request.UserId) && l.LoanDetail.Status < 200)
                        .Include(a => a.Applicants)
                        .Select(LoanDto.Projection)
                        .OrderByDescending(l => l.LoanDetail.Status)
                        .ToListAsync(cancellationToken),

                        CreateEnabled = false
                    };

                    // do some 3rd level subquery
                    // ThenInclude isn't returning consistently
                    foreach (var item in model.Loans) 
                    {
                        foreach (var appu in item.Applicants)
                        {
                            appu.ApplicantLessee = (from u in _context.AppUsers
                                                    where u.Id.Equals(appu.ApplicantLesseeId)
                                                    select u).Single();
                        }
               
                    };


                    break;
                default:
                    // show broadcasted
                    // loans
                    // TODO:// this is City/Location based showcasing 
                    //      of the loans
                    //      if they will apply for the loan
                    //      users will need to be authenticated.
                    model = new LoansListViewModel
                    {
                        Loans = await _context.Loans
                        .Where(l => l.LesseeId == null && l.LoanDetail.Status == 1)
                            .Select(LoanDto.Projection)
                            .OrderByDescending(l => l.LoanDetail.Status)
                            .ToListAsync(cancellationToken),

                        CreateEnabled = false
                    };

                    break;
            }

            return model;
        }
    }
}
