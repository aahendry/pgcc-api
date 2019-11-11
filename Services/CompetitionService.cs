using PgccApi.Models;
using PgccApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using PgccApi.Models.ViewModels;
using System.Threading.Tasks;
using AutoMapper;

namespace PgccApi.Services
{
    public interface ICompetitionService
    {
        Task<List<CompetitionTableRowViewModel>> GenerateTable(Competition competition);
    }

    public class CompetitionService : ICompetitionService
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;

        public CompetitionService(PgccContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CompetitionTableRowViewModel>> GenerateTable(Competition competition)
        {
            var currentSeason = await _context.Seasons.OrderByDescending(o => o.Name).FirstOrDefaultAsync();

            if (currentSeason == null)
            {
                throw new Exception("No Seasons found.");
            }

            var fixtures = await _context.Fixtures
                .Include(o => o.Team1)
                .Include(o => o.Team2)
                .Include(o => o.Season)
                .Include(o => o.Competition)
                .Where(o => o.CompetitionId == competition.Id && o.SeasonId == currentSeason.Id && !o.IsFinal).ToListAsync();

            if(fixtures.Count < 1)
            {
                return new List<CompetitionTableRowViewModel>();
            }

            var table = await _context.Rinks
                .Include(o => o.Season)
                .Include(o => o.Competition)
                .Where(o => o.CompetitionId == competition.Id && o.SeasonId == currentSeason.Id).Select(o => new CompetitionTableRowViewModel { Rink = _mapper.Map<RinkViewModel>(o) }).ToListAsync();

            foreach(var fixture in fixtures)
            {
                if(!fixture.Shots1.HasValue || !fixture.Shots2.HasValue) { continue; }

                table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Played++;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).For += fixture.Shots1.Value;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Against += fixture.Shots2.Value;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Shots += fixture.Shots1.Value - fixture.Shots2.Value;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).EndsWon += fixture.Ends1.Value;

                table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Played++;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).For += fixture.Shots2.Value;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Against += fixture.Shots1.Value;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Shots += fixture.Shots2.Value - fixture.Shots1.Value;
                table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).EndsWon += fixture.Ends2.Value;

                if (fixture.Shots1.Value > fixture.Shots2.Value)
                {
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Won++;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Points += 2;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Lost++;
                }
                else if (fixture.Shots2.Value > fixture.Shots1.Value)
                {
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Won++;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Points += 2;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Lost++;
                }
                else if (fixture.Shots2.Value == fixture.Shots1.Value)
                {
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Drawn++;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team1.Id).Points++;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Drawn++;
                    table.FirstOrDefault(o => o.Rink.Id == fixture.Team2.Id).Points++;
                }
            }

            return table.OrderByDescending(o => o.Points).ThenByDescending(o => o.EndsWon).ThenByDescending(o => o.Shots).ToList();
        }
    }
}