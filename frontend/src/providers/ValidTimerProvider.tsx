import { createContext, useContext, useEffect, useState } from "react";
import type { ValidUntil } from "@/pages/RoutesPage";
import { useQuery } from "@tanstack/react-query";
import api from "@/lib/axios";

type TimeLeft = {
    total: number;
    minutes: number;
    seconds: number;
};

const ValidTimerContext = createContext<TimeLeft | null>(null);


const fetchValidUntil = async (): Promise<ValidUntil> => {
    const res = await api.get("travel-prices/valid-until");
    return res.data;
};

export const ValidTimerProvider = ({
    children,
}: {
    children: React.ReactNode;
}) => {
    const [timeLeft, setTimeLeft] = useState<TimeLeft | null>(null);

    const { data: validUntil } = useQuery<ValidUntil>({
        queryKey: ["validUntil"],
        queryFn: fetchValidUntil,
        staleTime: timeLeft?.total,
        enabled: true,
    });

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
        <ValidTimerContext.Provider value={timeLeft}>
            {children}
        </ValidTimerContext.Provider>
    );
};

export const useValidTimer = () => {
    return useContext(ValidTimerContext);
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
