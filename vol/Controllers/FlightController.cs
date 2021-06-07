using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vol.Models.Entity;
using vol.Models.Interfaces;
using vol.ViewModel;

namespace vol.Controllers
{
    public class FlightController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlightController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Flight
        public async Task<IActionResult> Index()
        {

            var flights = await _unitOfWork.FlightRepository.GetAll().ToListAsync().ConfigureAwait(false);
            var flightViews = flights.Select(_ => new FlightView(_));

            return View(flightViews);
        }

        // GET: Flight/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (!id.HasValue )
            {
                return NotFound();
            }

            var flight = await _unitOfWork.FlightRepository.Get(id.Value).ConfigureAwait(false);

            if (flight == null)
            {
                return NotFound();
            }

            return View(new FlightView(flight));
        }

        // GET: Flight/Create
        public async Task<IActionResult> Create()
        {
            await FillViewData().ConfigureAwait(false);
            return View();
        }

        private async Task FillViewData(){
            var aeroports = await _unitOfWork.AeroportRepository.GetAll().ToListAsync().ConfigureAwait(false);
            var planes = await _unitOfWork.PlaneRepository.GetAll().ToListAsync().ConfigureAwait(false);

            ViewData["Arrival"] = new SelectList(aeroports , "Id", "Name");
            ViewData["Destination"] = new SelectList(aeroports, "Id", "Name");
            ViewData["Plane"] = new SelectList(planes, "Id", "Name");
        }
        // POST: Flight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaneId,DestinationId,ArrivalId,VoleTimeByHours,TakeOff,Id,CreatedDate,UpdatedDate")] Flight flight)
        {
            if (ModelState.IsValid && flight != null)
            {
                flight.CreatedDate = DateTime.Now;
                _unitOfWork.FlightRepository.Add(flight);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }
            
            await FillViewData().ConfigureAwait(false);
            return View(flight);
        }

        // GET: Flight/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var flight = await _unitOfWork.FlightRepository.Get(id.Value).ConfigureAwait(false);
            if (flight == null)
            {
                return NotFound();
            }
            await FillViewData().ConfigureAwait(false);
            return View(flight);
        }

        // POST: Flight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PlaneId,DestinationId,ArrivalId,VoleTimeByHours,TakeOff,Id,CreatedDate")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    flight.UpdatedDate = DateTime.Now;
                    _unitOfWork.FlightRepository.Update(flight);
                    await _unitOfWork.CommitAsync().ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await FillViewData().ConfigureAwait(false);
            return View(flight);
        }

        // GET: Flight/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var flight = await _unitOfWork.FlightRepository.Get(id.Value).ConfigureAwait(false);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var flight = await _unitOfWork.FlightRepository.Get(id).ConfigureAwait(false);
            _unitOfWork.FlightRepository.Delete(flight);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(long id)
        {
            return _unitOfWork.AeroportRepository.Get(id) != null;
        }
    }
}
