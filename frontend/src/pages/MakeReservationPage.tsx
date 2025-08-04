import { useLocation } from "react-router";
import type { TravelRoute } from "./RoutesPage";

export default function MakeReservationPage() {
  const location = useLocation();
  const travelRoute: TravelRoute = location.state?.travelRoute;

  window.scrollTo({ top: 0, left: 0, behavior: "auto" });

  if (!travelRoute) {
    return <p>Invalid access - no travel route data provided.</p>;
  }

  return (
    <div>
      <h1>Reserve total price {travelRoute.totalPrice}</h1>
    </div>
  );
}
