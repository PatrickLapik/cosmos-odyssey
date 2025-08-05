export type Destination = {
  id: string;
  name: string;
};

export type Company = {
  id: string;
  name: string;
};

export type Route = {
  id: string;
  from: string;
  fromId: string;
  to: string;
  toId: string;
};

export type ValidUntil = {
  validUntil: Date;
};

export type CompanyRouteResponse = {
  id: string;
  travelStart: Date;
  travelEnd: Date;
  price: number;
  company: Company;
  route: Route;
  validUntil: Date;
};

export type TravelRoute = {
  companyRouteResponses: CompanyRouteResponse[];
  totalPrice: number;
  totalTravelMinutes: number;
  totalDistance: number;
};

export type Reservation = {
    id: string;
    firstName: string;
    lastName: string;
    companyRoutes: Route[];
    totalPrice: number;
    totalTravelMinutes: number;
    companyNames: string[];
};
