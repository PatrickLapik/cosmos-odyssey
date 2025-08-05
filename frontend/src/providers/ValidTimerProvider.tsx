import { createContext, useContext, useEffect, useState } from "react";
import type { ValidUntil } from "@/types/ResponseTypes";
import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import api from "@/lib/axios";

type TimeLeft = {
  total: number;
  minutes: number;
  seconds: number;
};

type ValidTimerContextType = {
  timeLeft: TimeLeft;
  query: UseQueryResult<ValidUntil>;
};

const ValidTimerContext = createContext<ValidTimerContextType | null>(null);

const fetchValidUntil = async (): Promise<ValidUntil> => {
  const res = await api.get("travel-prices/valid-until");
  return res.data;
};

export const ValidTimerProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [timeLeft, setTimeLeft] = useState<TimeLeft>({
    total: 0,
    minutes: 0,
    seconds: 0,
  });

  const query = useQuery<ValidUntil>({
    queryKey: ["validUntil"],
    queryFn: fetchValidUntil,
    enabled: timeLeft.total <= 0,
  });

  const validUntil = query.data;

  useEffect(() => {
    if (!validUntil) return;

    const target = new Date(validUntil.validUntil + "Z");
    let interval: ReturnType<typeof setInterval>;

    const updateTime = () => {
      const remaining = getTimeRemaining(target);
      setTimeLeft(remaining);

      if (remaining.total <= 0) {
        clearInterval(interval);
      }
    };

    updateTime();
    interval = setInterval(updateTime, 1000);

    return () => clearInterval(interval);
  }, [validUntil]);

  return (
    <ValidTimerContext.Provider value={{ timeLeft, query }}>
      {children}
    </ValidTimerContext.Provider>
  );
};

export const useValidTimer = () => {
  const context = useContext(ValidTimerContext);
  if (!context) {
    throw new Error("useValidTimer must be used within a ValidTimerProvider");
  }
  return context;
};

function getTimeRemaining(targetDate: Date): TimeLeft {
  const total = targetDate.getTime() - new Date().getTime();

  const seconds = Math.max(Math.floor((total / 1000) % 60), 0);
  const minutes = Math.max(Math.floor((total / 1000 / 60) % 60), 0);

  return {
    total,
    minutes,
    seconds,
  };
}
